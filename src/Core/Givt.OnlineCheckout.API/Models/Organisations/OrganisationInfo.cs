namespace Givt.OnlineCheckout.API.Models.Organisations;

public class OrganisationInfo: OrganisationInfoBase
{
    public long Id { get; set; }
    public uint ConcurrencyToken { get; set; }
}
