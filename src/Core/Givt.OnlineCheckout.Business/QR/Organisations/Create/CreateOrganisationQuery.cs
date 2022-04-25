using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Create;

public class CreateOrganisationQuery: OrganisationModel, IRequest<OrganisationModel>
{
}
