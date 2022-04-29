using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Update;

public class UpdateOrganisationMediumTextsResult : LocalisableTextModel
{
    public int OrganisationId { get; set; }
    public string MediumId { get; set; }
}
