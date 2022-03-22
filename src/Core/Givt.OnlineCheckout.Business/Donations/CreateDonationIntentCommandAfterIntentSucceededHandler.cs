using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR.Pipeline;

namespace Givt.OnlineCheckout.API.Donations
{
    public record CreateDonationIntentCommandAfterIntentSucceededHandler(OnlineCheckoutContext DbContext) : IRequestPostProcessor<CreateDonationIntentCommand, CreateDonationIntentCommandResponse>
    {
        public async Task Process(CreateDonationIntentCommand request, CreateDonationIntentCommandResponse response, CancellationToken cancellationToken)
        {
            var dataDonation = new DonationData { 
                Amount = request.Amount, 
                PaymentProviderTransactionReference = request.AccountId 
            };

            await DbContext.Donations.AddAsync(dataDonation, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
