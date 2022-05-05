using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Organisations;

public class CreateOrganisationMediumTextsResponse : MediumTextsCore
{
    public int OrganisationId { get; set; }
    public string MediumId { get; set; }
    public string LanguageId { get; set; }
}