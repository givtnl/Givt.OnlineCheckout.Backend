using Givt.OnlineCheckout.Integrations.Interfaces;
using Microsoft.Extensions.Primitives;
using Stripe;
using Serilog.Sinks.Http.Logger;
using PaymentMethod = Givt.OnlineCheckout.Integrations.Interfaces.Models.PaymentMethod;
using System.Diagnostics;

namespace Givt.OnlineCheckout.Integrations.Stripe;

public class StripeIntegration : ISinglePaymentService
{
    private readonly StripeSettings _settings;
    private readonly ILog _log;

    public StripeIntegration(StripeSettings settings, ILog log)
    {
        _settings = settings;
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


    public ISinglePaymentEvent GetEventData(string content, IDictionary<string, StringValues> metaData)
    {
        StripeConfiguration.ApiKey = _settings.StripeApiKey;
        _log.Debug("Stripe event received: {0}", new object[] { content });
        var signature = metaData["Stripe-Signature"].ToString();
        _log.Debug("Stripe event signature: '{0}'", new object[] { signature });
        var stripeEvent = EventUtility.ConstructEvent(content, signature, _settings.EndpointSecret);
        //var stripeEvent = EventUtility.ConstructEvent(content, signature,
        //    "whsec_e5bd3b92653fc7989261589580ccad9a552643778dbeeb7c95c5acd1d66f46f2"); // secret for local session on QTLT02
        if (stripeEvent.Data.Object is PaymentIntent)
            return new StripeEventWrapper(stripeEvent);
        return null;
    }
}