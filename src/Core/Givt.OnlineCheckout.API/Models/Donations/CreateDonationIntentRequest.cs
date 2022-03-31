using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.API.Models.Donations;

public class CreateDonationIntentRequest
{
    public string Code { get; set; }
    public decimal Amount { get; set; }
    public string Medium { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public string Email { get; set; }
    public bool TaxReport { get; set; }
    public int? TimezoneOffset { get; set; } // TODO: make this a required parameter
    public string LanguageId { get; set; }
}