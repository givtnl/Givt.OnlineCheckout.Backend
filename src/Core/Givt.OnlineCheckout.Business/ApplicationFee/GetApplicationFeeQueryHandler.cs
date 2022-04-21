using Givt.OnlineCheckout.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.ApplicationFee;

public class GetApplicationFeeQueryHandler: IRequestHandler<GetApplicationFeeQuery, GetApplicationFeeQueryResponse>
{
    private readonly OnlineCheckoutContext _context;

    public GetApplicationFeeQueryHandler(OnlineCheckoutContext context)
    {
        _context = context;
    }
    
    public async Task<GetApplicationFeeQueryResponse> Handle(GetApplicationFeeQuery request, CancellationToken cancellationToken)
    {
        // This happens in a different query handler so we can implement logic that checks if there is a different application fee for this specific medium/campaign.
        return await _context.Mediums.Where(x => x.Medium == (string) request.MediumIdType)
            .Select(x => 
                new GetApplicationFeeQueryResponse
                {
                    ApplicationFeePercentage = x.Organisation.Country.ApplicationFeePercentage, 
                    ApplicationFeeFixedAmount = x.Organisation.Country.ApplicationFeeFixedAmount
                }).FirstAsync(cancellationToken);

    }
}