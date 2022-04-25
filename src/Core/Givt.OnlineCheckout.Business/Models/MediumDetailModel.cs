namespace Givt.OnlineCheckout.Business.Models;

public class MediumDetailModel
{
    public uint ConcurrencyToken { get; set; }
    public long OrganisationId { get; set; }
    public decimal[] Amounts { get; set; }
    public string Medium { get; set; }
    
}
