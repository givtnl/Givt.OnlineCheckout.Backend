using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Organisations.Update;

public class UpdateOrganisationRequest : OrganisationInfoCore, IConcurrency
{
    public uint ConcurrencyToken { get; set; }
}