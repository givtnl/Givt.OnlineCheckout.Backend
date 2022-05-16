namespace Givt.OnlineCheckout.Integrations.Interfaces.Models;

public class DonationsReport
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
    
    // Campaign info => This is for now like this, because we only send mails for one campaign
    public string CampaignName { get; set; }
    public GivtInfo Givt { get; set; }

    // donations
    public IEnumerable<Organisation> Organisations { get; set; }

}