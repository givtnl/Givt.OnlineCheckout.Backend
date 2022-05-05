namespace Givt.OnlineCheckout.API.Models.Organisations.Mediums.Texts.Update;

public class UpdateOrganisationMediumTextsResponse : MediumTextsInfo
{
    public int OrganisationId { get; set; }
    public string MediumId { get; set; }
}