namespace Givt.OnlineCheckout.Business.Models;

public class MediumDetailModel : MediumCoreModel, IConcurrency
{
    public long OrganisationId { get; set; }
    public uint ConcurrencyToken { get; set; }
}