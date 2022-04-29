namespace Givt.OnlineCheckout.API.Models.Organisations.Texts.Get;

public class GetOrganisationTextsRequest 
{
    public long OrganisationId { get; set; }
    public string LanguageId { get; set; }
}