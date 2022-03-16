using Givt.OnlineCheckout.Integrations.Interfaces;
using Stripe;
using PaymentMethod = Givt.OnlineCheckout.Integrations.Interfaces.Models.PaymentMethod;


namespace Givt.OnlineCheckout.Integrations.Stripe;

public class StripeIntegration: ISinglePaymentService
{
   public async Task<string> CreatePaymentIntent(decimal amount, decimal applicationFee, string accountId, PaymentMethod paymentMethod)
    {
        var requestOptions = new RequestOptions();
        requestOptions.ApiKey = "sk_test_51HmwjvLgFatYzb8pVz3bwCyC6EJK8V01LZOoqOsDlBwcdPMyyG2F0eW8k95JdbiEnSp3F7nWjpVuGuGvy8y17lGf00NCJZBLgq";

        StripeConfiguration.StripeClient = new StripeClient(
            requestOptions.ApiKey,
            httpClient: new SystemNetHttpClient(new HttpClient()));

        var service = new PaymentIntentService();
        
        var result = await service.CreateAsync(new PaymentIntentCreateOptions
        {
            Currency = "eur",
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
}