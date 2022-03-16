namespace Givt.OnlineCheckout.API.Requests.Merchants;

public class GetMediumResponse
{
    public string Medium { get; set; }
    public string OrganisationName { get; set; }
    public decimal[] Amounts { get; set; }
    public string ThankYou { get; set; }
    public string Goal { get; set; }
}