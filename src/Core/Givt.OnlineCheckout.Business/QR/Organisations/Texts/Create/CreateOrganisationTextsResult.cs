using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Texts.Create;

public class CreateOrganisationTextsResult : LocalisableTextsCore
{
    public int OrganisationId { get; set; }
    public string LanguageId { get; set; }
    public int ConcurrencyToken { get; set; }
}
