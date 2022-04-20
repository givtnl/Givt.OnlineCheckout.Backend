using Givt.OnlineCheckout.Integrations.Interfaces;
using MediatR;
using Serilog.Sinks.Http.Logger;
using Stripe;
using System.Diagnostics;
using PaymentMethod = Givt.OnlineCheckout.Integrations.Interfaces.Models.PaymentMethod;

namespace Givt.OnlineCheckout.Integrations.Stripe;

public class StripeIntegration : StripeIntegration<IRawSinglePaymentNotification>
{
    public StripeIntegration(StripeOptions settings, IMediator mediator, ILog log) :
        base(settings, mediator, log)
    { }
}

// Setup polymorphic dispatch, this handler is now a "generic" handler for every IRawSinglePaymentNotification
public class StripeIntegration<TNotification> : ISinglePaymentService, INotificationHandler<TNotification>
    where TNotification : IRawSinglePaymentNotification
{

    private const string SIGNATURE_HEADER_KEY = "Stripe-Signature";
    private readonly StripeOptions _settings;
    private readonly IMediator _mediator;
    private readonly ILog _log;

    public StripeIntegration(StripeOptions settings, IMediator mediator, ILog log)
    {
        _settings = settings;
        _mediator = mediator;
        _log = log;
    }

    public async Task<ISinglePaymentServicePaymentIntent> CreatePaymentIntent(string currency, decimal amount, decimal applicationFee, string accountId, PaymentMethod paymentMethod)
    {
        _log.Debug("Creating a Stripe Payment Intent: currency='{0}', amount='{1}', applicationFee='{2}', accountId='{3}', paymentMethod={4}",
            new object[] { currency, amount, applicationFee, accountId, paymentMethod });
        StripeConfiguration.ApiKey = _settings.StripeApiKey;
        StripeConfiguration.StripeClient = new StripeClient(
            _settings.StripeApiKey,
            httpClient: new SystemNetHttpClient(new HttpClient()));

        var service = new PaymentIntentService();
        string stripePaymentMethod;
        switch (paymentMethod)
        {
            case PaymentMethod.Bancontact:
            case PaymentMethod.Card:
            case PaymentMethod.Ideal:
            case PaymentMethod.Sofort:
            case PaymentMethod.Giropay:
            case PaymentMethod.EPS:
                // stripe payment method tags correspond to our PaymentMethod enum names for these
                stripePaymentMethod = paymentMethod.ToString().ToLower();
                break;
            case PaymentMethod.ApplePay:
            case PaymentMethod.GooglePay:
                stripePaymentMethod = "card"; // the Stripe UI will show ApplePay etc. automatically
                break;
            default:
                throw new NotSupportedException("Payment method not supported: " + paymentMethod.ToString());
        }
        var createOptions = new PaymentIntentCreateOptions
        {
            Currency = currency.ToLowerInvariant(),
            Amount = Convert.ToInt64(amount * 100),
            TransferData = new PaymentIntentTransferDataOptions() { Destination = accountId },
            ApplicationFeeAmount = Convert.ToInt64(applicationFee * 100),
            PaymentMethodTypes = new List<string> { stripePaymentMethod }
        };
        
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var paymentIntent = await service.CreateAsync(createOptions);
        stopwatch.Stop();
        _log.Debug("Stripe returned a payment intent, id='{0}' in {1} ms", new object[] { paymentIntent.Id, stopwatch.ElapsedMilliseconds });

        return new StripePaymentIntent(paymentIntent.Id, paymentIntent.ClientSecret);
    }

    public async Task Handle(TNotification notification, CancellationToken cancellationToken)
    {
        if (notification is not IRawSinglePaymentNotification rawNotification)
            return;

        Event stripeEvent;
        StripeConfiguration.ApiKey = _settings.StripeApiKey;
        _log.Debug("Stripe notification received: {0}", new object[] { rawNotification.RawData });
        if (rawNotification.MetaData.ContainsKey(SIGNATURE_HEADER_KEY))
        {
            try
            {
                var signature = rawNotification.MetaData[SIGNATURE_HEADER_KEY].ToString();
                _log.Debug("Stripe event signature: '{0}'", new object[] { signature });
                stripeEvent = EventUtility.ConstructEvent(rawNotification.RawData, signature, _settings.EndpointSecret);
            }
            catch (Exception ex)
            {
                _log.Warning("Failed to decode Stripe event data: {0}", new object[] { ex.Message });
                return;
            }
        }
        else
        {
            // call is not from Stripe itself, perhaps an attack / spoofing attempt?
            _log.Debug("No Stripe signature!");
            return;
        }
        if (stripeEvent == null)
        {
            _log.Debug("Failed to decode Stripe event data");
            return;
        }
        if (stripeEvent.Data.Object is not PaymentIntent)
        {
            _log.Debug("Stripe notification data does not contain PaymentIntent");
            return;
        }
        ISinglePaymentNotification cookedNotification = new StripePaymentNotification(stripeEvent);
        await _mediator.Publish(cookedNotification, cancellationToken);
    }

}