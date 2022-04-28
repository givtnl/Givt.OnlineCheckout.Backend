namespace Givt.OnlineCheckout.API.Models.Organisations.Mediums.Texts.Delete;

public class DeleteOrganisationMediumTextsRequest
{
    public long OrganisationId { get; set; }
    public string MediumId { get; set; }
    public string LanguageId { get; set; }
}