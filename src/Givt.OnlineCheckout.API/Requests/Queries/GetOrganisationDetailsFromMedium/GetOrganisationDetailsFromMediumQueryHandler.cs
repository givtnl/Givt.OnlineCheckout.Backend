using AutoMapper;
using Givt.OnlineCheckout.API.Models;
using Givt.OnlineCheckout.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace Givt.OnlineCheckout.API.Requests.Queries.GetOrganisationDetailsFromMedium;

public class GetOrganisationDetailsFromMediumQueryHandler: IRequestHandler<GetOrganisationDetailsFromMediumQuery, OrganisationDetails>
{
    private readonly OnlineCheckoutContext _context;
    private readonly IMapper _mapper;

    public GetOrganisationDetailsFromMediumQueryHandler(OnlineCheckoutContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<OrganisationDetails> Handle(GetOrganisationDetailsFromMediumQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<OrganisationDetails>(await _context.Organisations.FirstOrDefaultAsync(x => x.Namespace == request.Medium, cancellationToken));
    }
}