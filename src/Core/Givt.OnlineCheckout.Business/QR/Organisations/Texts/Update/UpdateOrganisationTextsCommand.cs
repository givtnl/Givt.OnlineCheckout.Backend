using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Texts.Update;

public class UpdateOrganisationTextsCommand : LocalisableTextsCore, IRequest<UpdateOrganisationTextsResult>
{
    public long OrganisationId { get; set; }
    public string LanguageId { get; set; }
    public uint ConcurrencyToken { get; set; }
}
