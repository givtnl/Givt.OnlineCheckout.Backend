using Givt.OnlineCheckout.API.Models;
using MediatR;

namespace Givt.OnlineCheckout.API.Requests.Queries.GetOrganisationDetailsFromMedium;

public class GetOrganisationDetailsFromMediumQuery: IRequest<OrganisationDetails>
{
    public string Medium { get; set; }
}