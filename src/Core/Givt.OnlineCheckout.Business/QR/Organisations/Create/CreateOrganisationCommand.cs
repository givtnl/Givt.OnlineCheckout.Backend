using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Create;

public class CreateOrganisationCommand: OrganisationCoreModel, IRequest<OrganisationDetailModel>
{
}
