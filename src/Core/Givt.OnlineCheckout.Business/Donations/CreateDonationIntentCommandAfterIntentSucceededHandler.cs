using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.Donations
{
    public record CreateDonationIntentCommandAfterIntentSucceededHandler(OnlineCheckoutContext DbContext) : IRequestPostProcessor<CreateDonationIntentCommand, CreateDonationIntentCommandResponse>
    {
        public async Task Process(CreateDonationIntentCommand request, CreateDonationIntentCommandResponse response, CancellationToken cancellationToken)
        {
            var dataDonation = new DonationData
            {
                Amount = request.Amount,
                TransactionReference = request.AccountId
            };

            // link to a donor if the user wants a tax report
            if (request.TaxReportRequested && !String.IsNullOrWhiteSpace(request.Email))
            {
                var email = request.Email.ToLower();
                var donor = await DbContext.Donors.FirstAsync(c => c.Email == email, cancellationToken: cancellationToken);
                if (donor != null)
                    donor = new DonorData { Email = email };
                dataDonation.Donor = donor;
            }

            await DbContext.Donations.AddAsync(dataDonation, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
