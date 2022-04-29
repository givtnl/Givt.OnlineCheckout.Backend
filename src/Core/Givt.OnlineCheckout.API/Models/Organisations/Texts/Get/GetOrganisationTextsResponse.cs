using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Organisations;

public class GetOrganisationTextsResponse : LocalisableTextsInfoCore, IConcurrency
{
    public uint ConcurrencyToken { get; set; }
}