namespace Givt.OnlineCheckout.Integrations.Interfaces.Models;

public enum PaymentMethod
{
    Bancontact = 0, // do not change unless the mapping in Stripe to stripe names, and the mapping to names in the UI is changed too
    Card = 1,       // idem
    Ideal = 2,      // idem
    Sofort = 3,     // idem
    Giropay = 4,    // idem
    EPS = 5,        // idem
    ApplePay = 6,   // idem
    GooglePay = 7,  // idem
}
