using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Texts.Update;

public class UpdateOrganisationTextsResult : LocalisableTextsCore
{
    public int OrganisationId { get; set; }
    public string LanguageId { get; set; }
    public uint ConcurrencyToken { get; set; }
}
