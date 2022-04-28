using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Organisations.Mediums.Texts.Update;

public class UpdateOrganisationMediumTextsRequest : LocalisableTextsInfoCore, IConcurrency
{
    public uint ConcurrencyToken { get; set; }
}