using Givt.OnlineCheckout.API.Models;
using MediatR;

namespace Givt.OnlineCheckout.API.Requests.Queries.GetOrganisationDetailsFromMedium;

public class GetOrganisationDetailsFromMediumQuery: IRequest<OrganisationDetails>
{
    public string Medium { get; set; }

    public static bool TryParse(string value, out GetOrganisationDetailsFromMediumQuery? query)
    {
        query = new GetOrganisationDetailsFromMediumQuery()
        {
            Medium = value
        };
        return true;
    }
}