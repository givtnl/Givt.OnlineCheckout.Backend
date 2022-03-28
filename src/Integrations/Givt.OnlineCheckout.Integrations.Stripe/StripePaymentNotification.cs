using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Stripe;

namespace Givt.OnlineCheckout.Integrations.Stripe
{
    internal class StripePaymentNotification : SinglePaymentNotification
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

        public override string TransactionReference => paymentIntent?.Id;

        public override bool Processing => stripeEvent?.Type == Events.PaymentIntentProcessing;
        public override bool Succeeded => stripeEvent?.Type == Events.PaymentIntentSucceeded;
        public override bool Cancelled => stripeEvent?.Type == Events.PaymentIntentCanceled;
        public override bool Failed => stripeEvent?.Type == Events.PaymentIntentPaymentFailed;
    }
}