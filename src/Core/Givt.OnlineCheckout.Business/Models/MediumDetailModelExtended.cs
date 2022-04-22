using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.Business.Models;

public class MediumDetailModelExtended
{
    public string OrganisationName { get; set; }
    public string OrganisationLogoLink { get; set; }
    public string Goal { get; set; }
    public string Medium { get; set; }
    public IEnumerable<PaymentMethod> PaymentMethods { get; set; }
    public string Currency { get; set; }
    public decimal[] Amounts { get; set; }
    public string ThankYou { get; set; }
}
