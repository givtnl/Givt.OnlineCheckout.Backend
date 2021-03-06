using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.List;

public class ListOrganisationMediumTextsQuery : IRequest<List<MediumTextModel>>
{
    public int OrganisationId { get; set; }
    public string MediumId { get; set; }
}
