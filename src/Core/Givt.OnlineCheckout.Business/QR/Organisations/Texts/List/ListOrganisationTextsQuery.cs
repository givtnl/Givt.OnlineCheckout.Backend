using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Texts.List;

public class ListOrganisationTextsQuery : IRequest<List<LocalisableTextModel>>
{
    public int OrganisationId { get; set; }
}
