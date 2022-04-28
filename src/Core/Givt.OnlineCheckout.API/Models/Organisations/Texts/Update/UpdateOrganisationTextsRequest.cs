using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Organisations;

public class UpdateOrganisationTextsRequest : LocalisableTextsCore
{
    public uint ConcurrencyToken { get; set; }
}