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
    public string Title { get; set; }
    public string Goal { get; set; }
    public string ThankYou { get; set; }
    public bool TaxDeductable { get; set; }
    public string RSIN { get; set; }
    public string HmrcReference { get; set; }
    public string CharityNumber { get; set; }
    public DateTime Timestamp { get; set; }
    public string Currency { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
}