using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Delete;

public class DeleteOrganisationMediumTextsCommand : IConcurrency, IRequest<bool>
{
    public string MediumId { get; set; }
    public string LanguageId { get; set; }
    public uint ConcurrencyToken { get; set; }
}