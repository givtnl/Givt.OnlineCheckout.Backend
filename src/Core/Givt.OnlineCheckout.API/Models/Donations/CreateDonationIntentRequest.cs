using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.API.Models.Donations;

public class CreateDonationIntentRequest
{    
    public string Currency { get; set; }
    public decimal Amount { get; set; }
    public string Medium { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public int TimezoneOffset { get; set; } 
}