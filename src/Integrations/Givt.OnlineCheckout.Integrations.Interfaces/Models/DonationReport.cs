namespace Givt.OnlineCheckout.Integrations.Interfaces.Models;

public class SingleDonationReport
{
    public string Locale { get; set; }
    public string OrganisationName { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public DateTime Date { get; set; }
}