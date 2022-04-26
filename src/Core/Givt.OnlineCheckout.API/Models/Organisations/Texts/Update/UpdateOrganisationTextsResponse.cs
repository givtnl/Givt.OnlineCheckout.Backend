using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Organisations;

public class UpdateOrganisationTextsResponse : LocalisableTextsCore
{
    public int OrganisationId { get; set; }
    public string LanguageId { get; set; }
}