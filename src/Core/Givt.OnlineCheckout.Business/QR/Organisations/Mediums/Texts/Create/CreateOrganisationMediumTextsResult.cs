using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Create;

public class CreateOrganisationMediumTextsResult : LocalisableTextsCore
{
    public int OrganisationId { get; set; }
    public string MediumId { get; set; }
    public string LanguageId { get; set; }
    public int ConcurrencyToken { get; set; }
}
