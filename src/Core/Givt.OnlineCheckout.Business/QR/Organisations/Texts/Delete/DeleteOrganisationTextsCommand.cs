using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Texts.Delete;

public class DeleteOrganisationTextsCommand : LocalisableTextsCore, IRequest<bool>
{
    public long OrganisationId { get; set; }
    public string LanguageId { get; set; }

}
