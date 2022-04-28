using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Mediums;

public class MediumInfo : MediumInfoCore, IConcurrency
{
    public long OrganisationId { get; set; }
    public uint ConcurrencyToken { get; set; }
}