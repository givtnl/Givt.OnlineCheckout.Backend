using Stripe;

namespace Givt.OnlineCheckout.Integrations.Stripe;

public static class PaymentIntentExtensions
{
    public static string GetPaymentMethod(this PaymentIntent paymentIntent)
    {
        return paymentIntent.Charges.SingleOrDefault()?.PaymentMethodDetails.Type;
    }

    public static string GetFingerprint(this PaymentIntent paymentIntent)
    {
        return paymentIntent.GetPaymentMethod().ToLower() switch
        {
            "ideal" => paymentIntent.Charges.Single().PaymentMethodDetails.Ideal.IbanLast4,
            "bancontact" => paymentIntent.Charges.Single().PaymentMethodDetails.Bancontact.IbanLast4,
            "sofort" => paymentIntent.Charges.Single().PaymentMethodDetails.Sofort.IbanLast4,
            "card" => paymentIntent.Charges.Single().PaymentMethodDetails.Card.Last4,
            "googlepay" => paymentIntent.Charges.Single().PaymentMethodDetails.Card.Fingerprint,
            "applepay" => paymentIntent.Charges.Single().PaymentMethodDetails.Card.Fingerprint,
            _ => string.Empty
        };
    }
}