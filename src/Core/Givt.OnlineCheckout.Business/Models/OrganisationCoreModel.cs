namespace Givt.OnlineCheckout.Business.Models;

public class OrganisationCoreModel
{
    public string Country { get; set; }
    public string Name { get; set; }
    public string PaymentProviderAccountReference { get; set; }
    public string Namespace { get; set; }
    public string LogoImageLink { get; set; }
    public IEnumerable<string> PaymentMethods { get; set; }
    public bool Active { get; set; }
    public bool TaxDeductable { get; set; }

    // NL
    public string RSIN { get; set; }

    // UK
    public string HmrcReference { get; set; }
    public string CharityNumber { get; set; }
}
