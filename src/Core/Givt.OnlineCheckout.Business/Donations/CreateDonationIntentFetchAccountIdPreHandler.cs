using Givt.OnlineCheckout.API.Exceptions;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.API.Donations;

public record CreateDonationIntentFetchAccountIdPreHandler(OnlineCheckoutContext DbContext): IRequestPreProcessor<CreateDonationIntentCommand>
{   
    public async Task Process(CreateDonationIntentCommand request, CancellationToken cancellationToken)
    {
        var medium = await DbContext.Mediums
            .Where(x => x.Medium == request.MediumId.ToString())
            .Include(x => x.Merchant)
            .FirstOrDefaultAsync(cancellationToken);

        if (medium == null)
            throw new NotFoundException(nameof(MediumData), request.MediumId);
       
        if (medium.Merchant.PaymentProviderAccountReference == null)
            throw new BadRequestException("Merchant has no account reference");

        request.AccountId = medium.Merchant.PaymentProviderAccountReference;
    }
}