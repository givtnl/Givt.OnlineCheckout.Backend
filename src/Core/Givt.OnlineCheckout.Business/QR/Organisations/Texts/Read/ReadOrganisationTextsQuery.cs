using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Texts.Read;

public class ReadOrganisationTextsQuery : LocalisableTextsCore, IRequest<ReadOrganisationTextsResult>
{
    public long OrganisationId { get; set; }
    public string LanguageId { get; set; }

}
