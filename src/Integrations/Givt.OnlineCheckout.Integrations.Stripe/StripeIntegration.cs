using Givt.OnlineCheckout.Integrations.Interfaces;
using Microsoft.Extensions.Primitives;
using Stripe;
using PaymentMethod = Givt.OnlineCheckout.Integrations.Interfaces.Models.PaymentMethod;


namespace Givt.OnlineCheckout.Integrations.Stripe;

public class StripeIntegration : ISinglePaymentService
{
    private readonly StripeSettings _settings;
    public StripeIntegration(StripeSettings settings)
    {
        _settings = settings;
    }

    public async Task<string> CreatePaymentIntent(string currency, decimal amount, decimal applicationFee, string accountId, PaymentMethod paymentMethod)
    {
        // TODO: logging
        StripeConfiguration.ApiKey = _settings.StripeApiKey;
        StripeConfiguration.StripeClient = new StripeClient(
            _settings.StripeApiKey,
            httpClient: new SystemNetHttpClient(new HttpClient()));

        var service = new PaymentIntentService();

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

        return result.ClientSecret;
    }


    public ISinglePaymentEvent GetEventData(string content, IDictionary<string, StringValues> metaData)
    {
        // TODO: logging
        StripeConfiguration.ApiKey = _settings.StripeApiKey;        
        //log.Verbose(content);
        var signature = metaData["Stripe-Signature"].ToString();
        //log.Verbose(signature);
        var stripeEvent = EventUtility.ConstructEvent(content, signature, _settings.EndpointSecret);
        return new StripeEventWrapper(stripeEvent);
    }
}