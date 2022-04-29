using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Read;

public class ReadOrganisationMediumQuery : IRequest<MediumDetailModel>
{
    public long OrganisationId { get; set; }
    public string MediumId { get; set; }
}
