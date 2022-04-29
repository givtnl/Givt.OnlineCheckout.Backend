namespace Givt.OnlineCheckout.Business.Models;

public class OrganisationDetailModel : OrganisationCoreModel, IConcurrency
{
    public long OrganisationId { get; set; }
    public uint ConcurrencyToken { get; set; }
}