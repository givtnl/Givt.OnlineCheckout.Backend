using Givt.OnlineCheckout.Persistance.Entities;

namespace Givt.OnlineCheckout.Integrations.Interfaces.Models;

public class Organisation
{
    public Organisation(OrganisationData source)
    {
        Name = source.Name;
        TaxDeductable = source.TaxDeductable;
        RSIN = source.RSIN;
        HmrcReference = source.HmrcReference;
        CharityNumber = source.CharityNumber;
    }

    public string Name { get; set; }
    public bool TaxDeductable { get; set; }
    public string RSIN { get; set; }
    public string HmrcReference { get; set; }
    public string CharityNumber { get; set; }

    public IEnumerable<Goal> Goals { get; set; }
    public IEnumerable<CurrencyAmount> TotalAmount { get; set; }
}
