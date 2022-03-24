using Givt.OnlineCheckout.Integrations.Interfaces;
using Stripe;

namespace Givt.OnlineCheckout.Integrations.Stripe
{
    internal class StripeEventWrapper : ISinglePaymentEvent
    {
        private readonly Event _stripeEvent;

        public StripeEventWrapper(Event stripeEvent)
        {
            _stripeEvent = stripeEvent;
        }

        public string TransactionReference => _stripeEvent.Id;

        public bool Succeeded => _stripeEvent.Type == Events.PaymentIntentSucceeded;

        public bool Cancelled => _stripeEvent.Type == Events.PaymentIntentCanceled;
    }
}