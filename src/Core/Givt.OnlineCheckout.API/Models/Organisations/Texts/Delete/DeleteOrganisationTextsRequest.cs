namespace Givt.OnlineCheckout.API.Models.Organisations.Texts.Delete;

public class DeleteOrganisationTextsRequest
{
    public long OrganisationId { get; set; }
    public string LanguageId { get; set; }
}