using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Texts.Delete;

public class DeleteOrganisationTextsCommand : IConcurrency, IRequest<bool>
{
    public long OrganisationId { get; set; }
    public string LanguageId { get; set; }
    public uint ConcurrencyToken { get; set; }
}
