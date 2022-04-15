using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Serilog.Sinks.Http.Logger;

namespace Givt.OnlineCheckout.Business.Donations
{
    public record CreateDonationIntentCommandAfterIntentSucceededHandler(OnlineCheckoutContext DbContext, ILog logger) :
        IRequestPostProcessor<CreateDonationIntentCommand, CreateDonationIntentCommandResponse>
    {
        public async Task Process(CreateDonationIntentCommand request, CreateDonationIntentCommandResponse response, CancellationToken cancellationToken)
        {
            var dataDonation = new DonationData
            {
                Amount = request.Amount,
                TransactionReference = response.TransactionReference,
                TransactionDate = DateTime.UtcNow,
                TimezoneOffset = request.TimezoneOffset,
                Medium = request.Medium,
                Currency = request.Currency
            };
            await DbContext.Donations.AddAsync(dataDonation, cancellationToken);
            var writeCount = await DbContext.SaveChangesAsync(cancellationToken);
            logger.Debug("Donation with reference '{0}' recorded, {1} records written",
                new object[] { response.TransactionReference, writeCount });
        }
    }
}
