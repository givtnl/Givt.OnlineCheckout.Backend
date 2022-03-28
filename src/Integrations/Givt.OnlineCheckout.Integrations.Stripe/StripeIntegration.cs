using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using MediatR;
using Serilog.Sinks.Http.Logger;
using Stripe;
using System.Diagnostics;
using PaymentMethod = Givt.OnlineCheckout.Integrations.Interfaces.Models.PaymentMethod;

namespace Givt.OnlineCheckout.Integrations.Stripe;

public class StripeIntegration : ISinglePaymentService
{
    private const string SIGNATURE_HEADER_KEY = "Stripe-Signature";
    private readonly StripeSettings _settings;
    private readonly IMediator _mediator;
    private readonly ILog _log;

    public StripeIntegration(StripeSettings settings, IMediator mediator, ILog log)
    {
        _settings = settings;
        _mediator = mediator;
        _log = log;
    }

    public async Task<string> CreatePaymentIntent(string currency, decimal amount, decimal applicationFee, string accountId, PaymentMethod paymentMethod)
    {
        _log.Debug("Creating a Stripe Payment Intent: currency='{0}', amount='{1}', applicationFee='{2}', accountId='{3}', paymentMethod={4}",
            new object[] { currency, amount, applicationFee, accountId });
        StripeConfiguration.ApiKey = _settings.StripeApiKey;
        StripeConfiguration.StripeClient = new StripeClient(
            _settings.StripeApiKey,
            httpClient: new SystemNetHttpClient(new HttpClient()));

        var service = new PaymentIntentService();
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var result = await service.CreateAsync(new PaymentIntentCreateOptions
        {
            Currency = currency.ToLowerInvariant(),
            Amount = Convert.ToInt64(amount * 100),
            TransferData = new PaymentIntentTransferDataOptions()
            {
                Destination = accountId
            },
            ApplicationFeeAmount = Convert.ToInt64(applicationFee * 100),
            PaymentMethodTypes = new List<string>
            {
                paymentMethod.ToString().ToLowerInvariant()
            }
        });
        stopwatch.Stop();
        _log.Debug("Stripe returned a payment intent, id='{0}' in {1} ms", new object[] { result.Id, stopwatch.ElapsedMilliseconds });
        return result.ClientSecret;
    }


    public Task Handle(RawSinglePaymentNotification rawNotification, CancellationToken cancellationToken)
    {
        Event stripeEvent;

        StripeConfiguration.ApiKey = _settings.StripeApiKey;
        _log.Debug("Stripe notification received: {0}", new object[] { rawNotification.RawData });
        if (rawNotification.MetaData.ContainsKey(SIGNATURE_HEADER_KEY))
        {
            try
            {
                var signature = rawNotification.MetaData[SIGNATURE_HEADER_KEY].ToString();
                _log.Debug("Stripe event signature: '{0}'", new object[] { signature });
                //stripeEvent = EventUtility.ConstructEvent(rawNotification.RawData, signature, _settings.EndpointSecret);
                stripeEvent = EventUtility.ConstructEvent(rawNotification.RawData, signature,
                    "whsec_e5bd3b92653fc7989261589580ccad9a552643778dbeeb7c95c5acd1d66f46f2"); // secret for local session on QTLT02
            }
            catch (Exception ex)
            {
                _log.Warning("Failed to decode Stripe event data: {0}", new object[] { ex.Message });
                return Task.CompletedTask;
            }
        }
        else
        {
            // call is not from Stripe itself, perhaps an attack / spoofing attempt?
            _log.Debug("No Stripe signature!");
            return Task.CompletedTask;
        }
        if (stripeEvent == null)
        {
            _log.Debug("Failed to decode Stripe event data");
            return Task.CompletedTask;
        }
        if (stripeEvent.Data.Object is not PaymentIntent)
        {
            _log.Debug("Stripe notification data does not contain PaymentIntent");
            return Task.CompletedTask;

        }
        var cookedNotification = new StripePaymentNotification(stripeEvent);
        return _mediator.Publish(cookedNotification, cancellationToken);
    }

}