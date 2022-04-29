using Givt.OnlineCheckout.API.Models.Mediums;
using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Organisations.Mediums.Update;

public class UpdateOrganisationMediumRequest : MediumInfoCore, IConcurrency
{
    public uint ConcurrencyToken { get; set; }
}
