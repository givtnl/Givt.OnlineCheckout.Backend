using MediatR;

namespace Givt.OnlineCheckout.Business.Organisations;

public class ListOrganisationsQuery : ListQuery, IRequest<List<OrganisationModel>>
{
}
