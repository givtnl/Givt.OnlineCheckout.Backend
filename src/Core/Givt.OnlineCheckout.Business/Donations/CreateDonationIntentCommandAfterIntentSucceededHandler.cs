using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Givt.OnlineCheckout.Business.Donations
{
    public record CreateDonationIntentCommandAfterIntentSucceededHandler(OnlineCheckoutContext DbContext, ILogger logger) :
        IRequestPostProcessor<CreateDonationIntentCommand, CreateDonationIntentCommandResponse>
    {
        public async Task Process(CreateDonationIntentCommand request, CreateDonationIntentCommandResponse response, CancellationToken cancellationToken)
        {
            var dataDonation = new DonationData
            {
                Amount = request.Amount,
                TransactionReference = request.AccountId,
                TransactionDate = DateTime.UtcNow,
                TimezoneOffset = request.TimezoneOffset ?? -120 // TODO: make TimezoneOffset a required parameter at the API, and remove the default of -120
            };

            // link to a donor if the user wants a tax report
            if (request.TaxReport && !String.IsNullOrWhiteSpace(request.Email))
            {
                logger.Debug("Registering tax report request for email address '{0}'", new object[] { request.Email });
                var email = request.Email.ToLower();
                var donor = await DbContext.Donors.FirstAsync(c => c.Email == email, cancellationToken: cancellationToken);
                if (donor != null)
                {
                    logger.Debug("Creating a new donor record for email address '{0}'", new object[] { request.Email });
                    donor = new DonorData { Email = email };
                }
                dataDonation.Donor = donor;
            }

            await DbContext.Donations.AddAsync(dataDonation, cancellationToken);
            var writeCount = await DbContext.SaveChangesAsync(cancellationToken);
            logger.Debug("Donation with reference '{0}' recorded, {1} records written", new object[] { request.AccountId, writeCount });
        }
    }
}
