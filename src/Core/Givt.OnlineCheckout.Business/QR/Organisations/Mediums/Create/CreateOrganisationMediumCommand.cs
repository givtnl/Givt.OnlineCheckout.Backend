using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Create;

public class CreateOrganisationMediumCommand: MediumDetailModel, IRequest<MediumDetailModel>
{
    public long OrganisationId { get; set; }

}
