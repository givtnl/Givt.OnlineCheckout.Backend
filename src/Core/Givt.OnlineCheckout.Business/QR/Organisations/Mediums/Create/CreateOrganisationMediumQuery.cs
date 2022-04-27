using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Create;

public class CreateOrganisationMediumQuery: MediumDetailModel, IRequest<MediumDetailModel>
{
}
