namespace Givt.OnlineCheckout.Integrations.Interfaces.Models;

public enum PaymentMethod // Shouln't this be a StripePaymentMethod ? 
{
    Bancontact,
    Card,
    Ideal,
    Sofort
}
