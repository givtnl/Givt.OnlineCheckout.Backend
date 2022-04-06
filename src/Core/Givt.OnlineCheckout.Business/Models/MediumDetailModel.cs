namespace Givt.OnlineCheckout.Business.Models;

public class MediumDetailModel
{
    public string OrganisationName { get; set; }
    public string OrganisationLogoLink { get; set; }
    public string Goal { get; set; }
    public string Medium { get; set; }
    public string Currency { get; set; }
    public decimal[] Amounts { get; set; }
    public string ThankYou { get; set; }

    // Language is used to select the best matching version of the Goal and ThankYou strings while mapping the data back to the API
    internal string Language { get; set; }
}
