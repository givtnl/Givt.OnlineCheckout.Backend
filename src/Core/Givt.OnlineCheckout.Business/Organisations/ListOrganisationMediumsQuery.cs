using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.Organisations;

public class ListOrganisationMediumsQuery : IRequest<List<MediumDetailModel>>
{
    public int OrganisationId { get; set; }
}
