using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Read;

public class ReadOrganisationMediumTextsQuery : MediumTextsCore, IRequest<ReadOrganisationMediumTextsResult>
{
    public long OrganisationId { get; set; }
    public string MediumId { get; set; }
    public string LanguageId { get; set; }

}
