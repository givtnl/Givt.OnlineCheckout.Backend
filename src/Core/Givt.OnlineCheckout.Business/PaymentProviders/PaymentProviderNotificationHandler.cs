using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Givt.OnlineCheckout.Persistance.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.PaymentProviders;

public record PaymentProviderNotificationHandler(ISinglePaymentService SinglePaymentService, OnlineCheckoutContext Context) :
    INotificationHandler<SinglePaymentNotification>
{
    async Task INotificationHandler<SinglePaymentNotification>.Handle(SinglePaymentNotification notification, CancellationToken cancellationToken)
    {

        // load the matching donation
        var donation = await Context.Donations
            .Where(donation => donation.TransactionReference == notification.TransactionReference)
            .FirstOrDefaultAsync(cancellationToken);
        if (donation == null)
            return; // donation not found

        // update donation status
        if (notification.Processing)
            donation.Status = DonationStatus.Processing;
        else if (notification.Succeeded)
        {
            if (donation.Status != DonationStatus.Succeeded)
            {
                donation.Status = DonationStatus.Succeeded;
                // send confirmation if TaxRefund requested
                if (donation.Donor != null) // donation is linked to donor, this only happens if a TaxReport is requested
                {
                    // send email
                }
            }
        }
        else if (notification.Cancelled)
            donation.Status = DonationStatus.Cancelled;
        else if (notification.Failed)
            donation.Status = DonationStatus.PaymentFailed;

        // write changes
        await Context.SaveChangesAsync(cancellationToken);
        return;
    }

}