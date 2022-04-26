using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Update;

public class UpdateOrganisationMediumTextsCommand : LocalisableTextsCore, IRequest<UpdateOrganisationMediumTextsResult>
{
    public int OrganisationId { get; set; }
    public string MediumId { get; set; }
    public string LanguageId { get; set; }

}
