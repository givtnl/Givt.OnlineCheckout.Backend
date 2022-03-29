using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Persistance.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog.Sinks.Http.Logger;

namespace Givt.OnlineCheckout.Business.PaymentProviders;

// Setup polymorphic dispatch, this handler is now a "generic" handler for every ISinglePaymentNotification
public class PaymentProviderNotificationHandler<TNotification> : INotificationHandler<TNotification>
    where TNotification : ISinglePaymentNotification
{
    private readonly ILog _log;
    private readonly OnlineCheckoutContext _context;

    public PaymentProviderNotificationHandler(ILog log, OnlineCheckoutContext context)
    {
        _log = log;
        _context = context;
    }

    public async Task Handle(TNotification notification, CancellationToken cancellationToken)
    {
        if (notification is not ISinglePaymentNotification spNotification)
            return;

        _log.Debug("PaymentProviderNotificationHandler.Handle SinglePaymentNotification");
        // load the matching donation
        var donation = await _context.Donations
            .Where(donation => donation.TransactionReference == spNotification.TransactionReference)
            .FirstOrDefaultAsync(cancellationToken);
        if (donation == null)
        {
            _log.Warning("No donation found with transaction reference '{0}'", new object[] { spNotification.TransactionReference });
            return; // donation not found
        }

        // update donation status
        if (notification.Processing)
            donation.Status = DonationStatus.Processing;
        else if (notification.Succeeded)
        {
            if (donation.Status != DonationStatus.Succeeded)
            {
                donation.Status = DonationStatus.Succeeded;
                // TODO: donation.TransactionDate = notification.TransactionDate;
                _log.Debug("Donation with transaction reference '{0}' set to status {1}",
                    new object[] { spNotification.TransactionReference, donation.Status });

                // send confirmation if TaxRefund requested
                if (!String.IsNullOrWhiteSpace(donation.Donor?.Email)) // donation is linked to donor (this only happens if a TaxReport is requested) and email is filled in
                {
                    // collect information to build confirmation email/report
                    // send email
                    _log.Debug("Sending email to {1} for donation with transaction reference '{0}'",
                        new object[] { spNotification.TransactionReference, donation.Donor.Email });
                }
            }
        }
        else if (notification.Cancelled)
        {
            donation.Status = DonationStatus.Cancelled;
            _log.Debug("Donation with transaction reference '{0}' set to status {1}",
                new object[] { spNotification.TransactionReference, donation.Status });
        }
        else if (notification.Failed)
        {
            donation.Status = DonationStatus.PaymentFailed;
            _log.Debug("Donation with transaction reference '{0}' set to status {1}",
                new object[] { spNotification.TransactionReference, donation.Status });
        }

        // write changes
        await _context.SaveChangesAsync(cancellationToken);
        return;
    }

}