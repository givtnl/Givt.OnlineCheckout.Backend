using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.API.Models.Donations;

public class CreateDonationIntentRequest
{
    public decimal Amount { get; set; }
    public string Medium { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}