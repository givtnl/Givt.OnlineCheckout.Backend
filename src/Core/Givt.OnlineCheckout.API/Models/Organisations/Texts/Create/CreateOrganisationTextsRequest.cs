using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Organisations;

public class CreateOrganisationTextsRequest : LocalisableTextsCore
{
    public int ConcurrencyToken { get; set; }
}