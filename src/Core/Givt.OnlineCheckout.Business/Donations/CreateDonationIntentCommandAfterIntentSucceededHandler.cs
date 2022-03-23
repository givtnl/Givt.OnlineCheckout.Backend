using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.API.Donations
{
    public record CreateDonationIntentCommandAfterIntentSucceededHandler(OnlineCheckoutContext DbContext) : IRequestPostProcessor<CreateDonationIntentCommand, CreateDonationIntentCommandResponse>
    {
        public async Task Process(CreateDonationIntentCommand request, CreateDonationIntentCommandResponse response, CancellationToken cancellationToken)
        {
            var dataDonation = new DonationData
            {
                Amount = request.Amount,
                PaymentProviderTransactionReference = request.AccountId
            };
            // link to a customer if the user wants a tax report
            if (request.TaxReportRequested == true && !String.IsNullOrWhiteSpace(request.Email))
            {
                var email = request.Email.ToLower();
                var c = await DbContext.Customers.FirstAsync(c => c.Email == email, cancellationToken: cancellationToken);
                if (c != null)
                    c = new CustomerData { Email = email };
                dataDonation.Customer = c;
            }

            await DbContext.Donations.AddAsync(dataDonation, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
