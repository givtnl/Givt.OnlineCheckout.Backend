using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Texts.Read;

public class ReadOrganisationTextsResult : LocalisableTextsCore
{
    public int OrganisationId { get; set; }
    public string LanguageId { get; set; }
    public uint ConcurrencyToken { get; set; }
}
