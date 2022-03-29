using Givt.OnlineCheckout.Integrations.Interfaces;

using Stripe;

namespace Givt.OnlineCheckout.Integrations.Stripe
{
    internal class StripePaymentNotification : ISinglePaymentNotification
    {
        private readonly Event stripeEvent;
        private readonly PaymentIntent paymentIntent;

        public StripePaymentNotification(Event stripeEvent)
        {
            // Stripe json deserialisation has constructed both Event and inner Event.Data.Object,
            // so we can just cast the Data.Object to a PaymentIntent
            this.stripeEvent = stripeEvent;            
            paymentIntent = stripeEvent.Data.Object as PaymentIntent;
        }

        public string TransactionReference => paymentIntent?.Id;

        // TODO: this is the datetime of the creation of the payment intent. Should get the transaction datetime
        public DateTime? TransactionDate { get => paymentIntent.Created; }

        public bool Processing => stripeEvent?.Type == Events.PaymentIntentProcessing;
        public bool Succeeded => stripeEvent?.Type == Events.PaymentIntentSucceeded;
        public bool Cancelled => stripeEvent?.Type == Events.PaymentIntentCanceled;
        public bool Failed => stripeEvent?.Type == Events.PaymentIntentPaymentFailed;

    }
}