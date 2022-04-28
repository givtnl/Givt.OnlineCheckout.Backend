using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Organisations;

public class OrganisationInfo : OrganisationInfoCore, IConcurrency
{
    public long OrganisationId { get; set; }
    public uint ConcurrencyToken { get; set; }
}
