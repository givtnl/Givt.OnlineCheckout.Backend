namespace Givt.OnlineCheckout.API.Models.Mediums;

public class GetMediumResponse
{
    public string OrganisationName { get; set; }
    public string Country { get; set; }
    public string OrganisationLogoLink { get; set; }
    public string Title { get; set; }
    public string Goal { get; set; }
    public string ThankYou { get; set; }
    public string[] PaymentMethods { get; set; }
    public string Currency { get; set; }
    public decimal[] Amounts { get; set; }

}