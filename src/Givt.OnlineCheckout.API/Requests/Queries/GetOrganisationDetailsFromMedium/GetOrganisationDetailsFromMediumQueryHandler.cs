using Givt.OnlineCheckout.API.Models;
using MediatR;
using NuGet.Common;

namespace Givt.OnlineCheckout.API.Requests.Queries.GetOrganisationDetailsFromMedium;

public class GetOrganisationDetailsFromMediumQueryHandler: IRequestHandler<GetOrganisationDetailsFromMediumQuery, OrganisationDetails>
{
    public async Task<OrganisationDetails> Handle(GetOrganisationDetailsFromMediumQuery request, CancellationToken cancellationToken)
    {
        return new OrganisationDetails
        {
            Name = "TestOrganisation",
            Currency = "USD"
        };
    }
}