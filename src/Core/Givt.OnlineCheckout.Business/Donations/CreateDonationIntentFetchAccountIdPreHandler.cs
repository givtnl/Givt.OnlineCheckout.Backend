using Givt.OnlineCheckout.Infrastructure.DbContexts;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Application.Donations;

public class CreateDonationIntentFetchAccountIdPreHandler: IRequestPreProcessor<CreateDonationIntentCommand>
{
    private readonly OnlineCheckoutContext _context;

    public CreateDonationIntentFetchAccountIdPreHandler(OnlineCheckoutContext context)
    {
        _context = context;
    }
    
    public async Task Process(CreateDonationIntentCommand request, CancellationToken cancellationToken)
    {
        var medium = await _context.Mediums.Where(x => x.Medium == request.Medium).Include(x => x.Merchant).FirstOrDefaultAsync(cancellationToken);

        if (medium == null)
        {
            throw new Exception("Merchant not found for this medium");
        }

        if (medium.Merchant.PaymentProviderAccountReference == null)
        {
            throw new Exception("Merchant has no account reference");
        }

        request.AccountId = medium.Merchant.PaymentProviderAccountReference;
    }
}