using Givt.OnlineCheckout.Integrations.Interfaces;

namespace Givt.OnlineCheckout.Integrations.Stripe
{
    internal class StripePaymentIntent : ISinglePaymentServicePaymentIntent
    {
        public string TransactionReference { get; }
        public string ClientToken { get; }

        public StripePaymentIntent(string transactionReference, string clientSecret)
        {
            TransactionReference = transactionReference;
            ClientToken = clientSecret;
        }
    }
}
