using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Create;

public class CreateOrganisationMediumTextsResult : MediumTextModel
{
    public int OrganisationId { get; set; }
    public string MediumId { get; set; }
}
