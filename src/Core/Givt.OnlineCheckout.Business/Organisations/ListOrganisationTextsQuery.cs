using MediatR;

namespace Givt.OnlineCheckout.Business.Organisations;

public class ListOrganisationTextsQuery : IRequest<List<LocalisableTextModel>>
{
    public int OrganisationId { get; set; }
}
