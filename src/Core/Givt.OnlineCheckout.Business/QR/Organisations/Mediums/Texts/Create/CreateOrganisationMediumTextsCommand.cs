using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Create;

public class CreateOrganisationMediumTextsCommand : MediumTextsCore, IRequest<CreateOrganisationMediumTextsResult>
{
    public long OrganisationId { get; set; }
    public string MediumId { get; set; }
    public string LanguageId { get; set; }

}
