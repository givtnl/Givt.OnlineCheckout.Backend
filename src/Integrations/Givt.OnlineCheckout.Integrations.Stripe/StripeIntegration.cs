using Givt.OnlineCheckout.Integrations.Interfaces;
using Microsoft.Extensions.Primitives;
using Stripe;
using PaymentMethod = Givt.OnlineCheckout.Integrations.Interfaces.Models.PaymentMethod;


namespace Givt.OnlineCheckout.Integrations.Stripe;

public class StripeIntegration : ISinglePaymentService
{
    private string apiKey = "sk_test_51HmwjvLgFatYzb8pVz3bwCyC6EJK8V01LZOoqOsDlBwcdPMyyG2F0eW8k95JdbiEnSp3F7nWjpVuGuGvy8y17lGf00NCJZBLgq";
    private string endpointSecret = "whsec_...."; // TODO: get from Stripe Dashboard

    public async Task<string> CreatePaymentIntent(string currency, decimal amount, decimal applicationFee, string accountId, PaymentMethod paymentMethod)
    {
        //var requestOptions = new RequestOptions();
        //requestOptions.ApiKey = apiKey;

        StripeConfiguration.StripeClient = new StripeClient(
            // requestOptions.ApiKey,
            apiKey,
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


    public async Task<ISinglePaymentEvent> GetEventData(Stream s, IDictionary<string, StringValues> metaData)
    {
        var json = await new StreamReader(s).ReadToEndAsync();
        var signature = metaData["Stripe-Signature"].ToString();
        var stripeEvent = EventUtility.ConstructEvent(json, signature, endpointSecret);
        return new StripeEventWrapper(stripeEvent);
    }

}