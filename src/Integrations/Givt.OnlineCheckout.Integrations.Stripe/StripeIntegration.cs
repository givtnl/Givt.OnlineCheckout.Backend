using Givt.OnlineCheckout.Integrations.Interfaces;
using MediatR;
using Serilog.Sinks.Http.Logger;
using Stripe;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PaymentMethod = Givt.OnlineCheckout.Integrations.Interfaces.Models.PaymentMethod;
using System.Text.RegularExpressions;

namespace Givt.OnlineCheckout.Integrations.Stripe;

// Setup polymorphic dispatch, this handler is now a "generic" handler for every IRawSinglePaymentNotification
public class StripeIntegration : ISinglePaymentService
{
    private const string SignatureHeaderKey = "Stripe-Signature";
    private readonly StripeOptions _settings;
    private readonly IMediator _mediator;
    private readonly ILog _log;

    public StripeIntegration(StripeOptions settings, IMediator mediator, ILog log)
    {
        _settings = settings;
        _mediator = mediator;
        _log = log;
    }

    public async Task<ISinglePaymentServicePaymentIntent> CreatePaymentIntent(
        string currency, decimal amount,
        decimal applicationFee,
        string description,
        string accountId, PaymentMethod paymentMethod)
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
            Description = description,
            TransferData = new PaymentIntentTransferDataOptions() { Destination = accountId },
            ApplicationFeeAmount = Convert.ToInt64(applicationFee * 100),
            PaymentMethodTypes = new List<string> { stripePaymentMethod },
            StatementDescriptor = SanitizeDescriptor(description, 5, 22), // non-card
            StatementDescriptorSuffix = SanitizeDescriptor(description, 2, 22 - ("Givt*".Length)), // card. "Givt" is what is configured in the dashboard, '*' is added as separator
        };

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var paymentIntent = await service.CreateAsync(createOptions);
        stopwatch.Stop();
        _log.Debug("Stripe returned a payment intent, id='{0}' in {1} ms", new object[] { paymentIntent.Id, stopwatch.ElapsedMilliseconds });

        return new StripePaymentIntent(paymentIntent.Id, paymentIntent.ClientSecret);
    }

    private string SanitizeDescriptor(string descriptor, int minLength, int maxLength)
    {
        var result = Regex.Replace(descriptor, "[<>\\'\"*]", string.Empty, RegexOptions.Compiled).Trim();
        if (result.Length < minLength)
            return null;
        if (result.Length > maxLength)
            return result.Substring(0, maxLength);
        return result;
    }

    public bool CanHandle(IHeaderDictionary headerDictionary)
    {
        return headerDictionary.ContainsKey(SignatureHeaderKey);
    }

    public ISinglePaymentNotification ConstructNotification(string json, IHeaderDictionary headerDictionary)
    {
        Event stripeEvent = EventUtility.ConstructEvent(json, headerDictionary[SignatureHeaderKey], _settings.EndpointSecret);

        if (stripeEvent == null)
        {
            _log.Warning($"Unable to decode Stripe event data", new object[] { json });
            throw new Exception($"Unable to decode Stripe event data {json}");
        }

        if (stripeEvent?.Data.Object is not PaymentIntent)
        {
            _log.Warning($"Stripe notification data does not contain a PaymentIntent", new object[] { stripeEvent.Data.Object });
            throw new Exception($"Stripe notification does not contain a PaymentIntent. {JsonConvert.SerializeObject(stripeEvent.Data.Object)}");
        }

        return new StripePaymentNotification(stripeEvent);
    }
}

