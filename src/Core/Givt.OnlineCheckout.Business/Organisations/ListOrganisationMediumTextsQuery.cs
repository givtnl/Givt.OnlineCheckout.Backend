using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.Organisations;

public class ListOrganisationMediumTextsQuery : IRequest<List<LocalisableTextModel>>
{
    public int OrganisationId { get; set; }
    public string MediumId { get; set; }
}
