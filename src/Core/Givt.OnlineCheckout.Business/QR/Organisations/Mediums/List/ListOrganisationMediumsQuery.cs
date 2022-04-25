using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.List;

public class ListOrganisationMediumsQuery : IRequest<List<MediumDetailModel>>
{
    public int OrganisationId { get; set; }
}
