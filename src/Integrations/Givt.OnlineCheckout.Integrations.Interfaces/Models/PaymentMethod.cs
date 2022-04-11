namespace Givt.OnlineCheckout.Integrations.Interfaces.Models;

public enum PaymentMethod
{
    Bancontact = 0, // do not change unless the mapping in Stripe to stripe names is changed too
    Card = 1,       // idem
    Ideal = 2,      // idem
    Sofort = 3,     // idem
    Giropay = 4,    // idem
    EPS = 5,        // idem
    ApplePay = 6,
    GooglePay = 7,
}
