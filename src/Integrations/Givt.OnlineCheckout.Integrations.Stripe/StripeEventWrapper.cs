using Givt.OnlineCheckout.Integrations.Interfaces;
using Stripe;

namespace Givt.OnlineCheckout.Integrations.Stripe
{
    internal class StripeEventWrapper : ISinglePaymentEvent
    {
        private readonly Event stripeEvent;
        private readonly PaymentIntent paymentIntent;

        public StripeEventWrapper(Event stripeEvent)
        {
            // Stripe json deserialisation has constructed both Event and inner Event.Data.Object,
            // so we can just cast the Data.Object to a PaymentIntent
            this.stripeEvent = stripeEvent;            
            paymentIntent = stripeEvent.Data.Object as PaymentIntent;
        }

        public string TransactionReference => paymentIntent?.Id;

        public bool Processing => stripeEvent.Type == Events.PaymentIntentProcessing;
        public bool Succeeded => stripeEvent.Type == Events.PaymentIntentSucceeded;
        public bool Cancelled => stripeEvent.Type == Events.PaymentIntentCanceled;
        public bool Failed => stripeEvent.Type == Events.PaymentIntentPaymentFailed;
    }
}