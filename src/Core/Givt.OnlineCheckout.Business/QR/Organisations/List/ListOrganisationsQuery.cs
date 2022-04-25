using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.List;

public class ListOrganisationsQuery : ListQuery, IRequest<List<OrganisationModel>>
{
}
