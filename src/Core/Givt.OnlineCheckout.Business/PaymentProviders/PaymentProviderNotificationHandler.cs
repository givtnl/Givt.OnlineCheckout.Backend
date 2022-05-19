using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Persistance.Entities;
using Givt.OnlineCheckout.Persistance.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog.Sinks.Http.Logger;

namespace Givt.OnlineCheckout.Business.PaymentProviders;

// Setup polymorphic dispatch, this handler is now a "generic" handler for every ISinglePaymentNotification
public class PaymentProviderNotificationHandler<TPaymentNotification> : INotificationHandler<TPaymentNotification>
    where TPaymentNotification : ISinglePaymentNotification
{
    private readonly ILog _log;
    //private readonly IConfiguration _configuration;
    private readonly OnlineCheckoutContext _context;
    //private readonly IMediator _mediator;

    public PaymentProviderNotificationHandler(ILog log, /*IConfiguration config, */OnlineCheckoutContext context/*, IMediator mediator*/)
    {
        _log = log;
        //_configuration = config;
        _context = context;
        //_mediator = mediator;
    }

    public async Task Handle(TPaymentNotification notification, CancellationToken cancellationToken)
    {
        if (notification is not ISinglePaymentNotification spNotification)
            return;

        _log.Information("PaymentProviderNotificationHandler.Handle SinglePaymentNotification");
        // load the matching donation
        var donation = await _context.Donations
            .Where(donation => donation.TransactionReference == spNotification.TransactionReference)
            .FirstOrDefaultAsync(cancellationToken);
        if (donation == null)
        {
            _log.Warning("No donation found with transaction reference '{0}'", new object[] { spNotification.TransactionReference });
            return; // donation not found
        }

        await UpdatePaymentStatus(spNotification, donation, cancellationToken);

        return;
    }

    private async Task UpdatePaymentStatus(ISinglePaymentNotification notification, DonationData donation, CancellationToken cancellationToken)
    {
        // update donation status
        if (notification.Processing)
            donation.Status = DonationStatus.Processing;
        else if (notification.Cancelled)
            donation.Status = DonationStatus.Cancelled;
        else if (notification.Failed)
            donation.Status = DonationStatus.PaymentFailed;
        else if (notification.Succeeded)
            HandleNotificationSucceeded(notification, donation);

        // write changes
        await _context.SaveChangesAsync(cancellationToken);

        if (!notification.Processing)
            _log.Information("Donation with transaction reference '{0}' set to status {1}",
                new object[] { notification.TransactionReference, donation.Status });
    }

    private static void HandleNotificationSucceeded(ISinglePaymentNotification notification, DonationData donation)
    {
        if (donation.Status == DonationStatus.Succeeded) return;

        donation.Status = DonationStatus.Succeeded;
        donation.TransactionDate = notification.TransactionDate;
        donation.PaymentMethod = (PaymentMethod)notification.PaymentMethod;
        donation.Last4 = notification.Last4;
        donation.Fingerprint = notification.Fingerprint;
    }
}