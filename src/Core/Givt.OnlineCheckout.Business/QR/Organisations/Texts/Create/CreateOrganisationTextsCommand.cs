using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Texts.Create;

public class CreateOrganisationTextsCommand : LocalisableTextsCore, IRequest<CreateOrganisationTextsResult>
{
    public long OrganisationId { get; set; }
    public string LanguageId { get; set; }

}
