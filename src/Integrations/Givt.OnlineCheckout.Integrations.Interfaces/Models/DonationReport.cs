namespace Givt.OnlineCheckout.Integrations.Interfaces.Models;

public class DonationReport
{
    // requested report langage / region
    public string Locale { get; set; }

    // donor info
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    // donations
    public string OrganisationName { get; set; }
    public string Goal { get; set; }
    public string Timestamp { get; set; }
    public string Currency { get; set; }
    public string Amount { get; set; }
}