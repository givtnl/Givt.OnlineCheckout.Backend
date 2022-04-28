using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Update;

public class UpdateOrganisationMediumCommand : MediumDetailModel, IRequest<MediumDetailModel>
{
}
