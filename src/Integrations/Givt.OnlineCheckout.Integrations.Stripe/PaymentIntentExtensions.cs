using Stripe;

namespace Givt.OnlineCheckout.Integrations.Stripe;

public static class PaymentIntentExtensions
{
    public static string GetPaymentMethod(this PaymentIntent paymentIntent)
    {
        return paymentIntent.Charges.Any() && paymentIntent.Charges.Count() == 1 ? paymentIntent.Charges.Single().PaymentMethodDetails.Type : "";
    }

    public static string GetFingerprint(this PaymentIntent paymentIntent)
    {
        if (string.IsNullOrEmpty(paymentIntent.GetPaymentMethod())) return string.Empty;
        
        var paymentMethodDetails = paymentIntent.GetSinglePaymentMethodDetail();
            
        return paymentIntent.GetPaymentMethod().ToLower() switch
        {
            "ideal" => GetIdealFingerprint(paymentMethodDetails),
            "bancontact" => GetBancontactFingerprint(paymentMethodDetails),
            "sofort" => GetSofortFingerprint(paymentMethodDetails),
            "card" => GetCardFingerprint(paymentMethodDetails),
            "googlepay" => GetGooglePayFingerprint(paymentMethodDetails),
            "applepay" => GetApplePayFingerprint(paymentMethodDetails),
            _ => string.Empty
        };

    }

    private static ChargePaymentMethodDetails GetSinglePaymentMethodDetail(this PaymentIntent paymentIntent)
    {
        return paymentIntent.Charges.Single().PaymentMethodDetails;
    }

    private static string GetIdealFingerprint(this ChargePaymentMethodDetails chargePaymentMethodDetails)
    {
        return chargePaymentMethodDetails.Ideal.IbanLast4;
    }

    private static string GetBancontactFingerprint(this ChargePaymentMethodDetails chargePaymentMethodDetails)
    {
        return chargePaymentMethodDetails.Bancontact.IbanLast4;
    }

    private static string GetSofortFingerprint(this ChargePaymentMethodDetails chargePaymentMethodDetails)
    {
        return chargePaymentMethodDetails.Sofort.IbanLast4;
    }

    private static string GetCardFingerprint(this ChargePaymentMethodDetails chargePaymentMethodDetails)
    {
        return chargePaymentMethodDetails.Card.Last4;
    }

    private static string GetGooglePayFingerprint(this ChargePaymentMethodDetails chargePaymentMethodDetails)
    {
        return chargePaymentMethodDetails.Card.Fingerprint;
    }

    private static string GetApplePayFingerprint(this ChargePaymentMethodDetails chargePaymentMethodDetails)
    {
        return chargePaymentMethodDetails.Card.Fingerprint;
    }
}