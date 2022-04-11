using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.API.Models.Mediums;

public class GetMediumResponse
{
    public string Medium { get; set; }
    public string OrganisationName { get; set; }
    public string OrganisationLogoLink { get; set; }
    public string Goal { get; set; }
    public PaymentMethod[] PaymentMethods { get; set; }
    public string Currency { get; set; }
    public decimal[] Amounts { get; set; }
    public string ThankYou { get; set; }

}