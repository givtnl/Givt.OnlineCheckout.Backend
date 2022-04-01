using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Business.Models.Report;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Persistance.Entities;
using Givt.OnlineCheckout.Persistance.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.Http.Logger;
using System.Globalization;

namespace Givt.OnlineCheckout.Business.PaymentProviders;

// Setup polymorphic dispatch, this handler is now a "generic" handler for every ISinglePaymentNotification
public class PaymentProviderNotificationHandler<TPaymentNotification> : INotificationHandler<TPaymentNotification>
    where TPaymentNotification : ISinglePaymentNotification
{
    private const string TEMPLATE_NAME = "Some Template Name";

    private readonly ILogger _log;
    private readonly OnlineCheckoutContext _context;
    private readonly IMediator _mediator;

    public ILogger Log => _log;

    public PaymentProviderNotificationHandler(ILogger log, OnlineCheckoutContext context, IMediator mediator)
    {
        _log = log;
        _context = context;
        _mediator = mediator;
    }

    public async Task Handle(TPaymentNotification notification, CancellationToken cancellationToken)
    {
        if (notification is not ISinglePaymentNotification spNotification)
            return;

        Log.Debug("PaymentProviderNotificationHandler.Handle SinglePaymentNotification");
        // load the matching donation
        var donation = await _context.Donations
            .Where(donation => donation.TransactionReference == spNotification.TransactionReference)
            .FirstOrDefaultAsync(cancellationToken);
        if (donation == null)
        {
            Log.Warning("No donation found with transaction reference '{0}'", new object[] { spNotification.TransactionReference });
            return; // donation not found
        }

        await UpdatePaymentStatus(spNotification, donation, cancellationToken);

        // send confirmation if TaxRefund requested
        if (!String.IsNullOrWhiteSpace(donation.Donor?.Email)) // donation is linked to donor (this only happens if a TaxReport is requested) and email is filled in
        {
            // collect information to build confirmation email/report
            await SendEmail(donation, cancellationToken);
        }

        return;
    }

    private async Task UpdatePaymentStatus( ISinglePaymentNotification notification, DonationData donation, CancellationToken cancellationToken)
    {
        // update donation status
        if (notification.Processing)
            donation.Status = DonationStatus.Processing;
        else if (notification.Succeeded)
        {
            if (donation.Status != DonationStatus.Succeeded)
            {
                donation.Status = DonationStatus.Succeeded;
                donation.TransactionDate = notification.TransactionDate;
                Log.Debug("Donation with transaction reference '{0}' set to status {1}",
                    new object[] { notification.TransactionReference, donation.Status });
            }
        }
        else if (notification.Cancelled)
        {
            donation.Status = DonationStatus.Cancelled;
            Log.Debug("Donation with transaction reference '{0}' set to status {1}",
                new object[] { notification.TransactionReference, donation.Status });
        }
        else if (notification.Failed)
        {
            donation.Status = DonationStatus.PaymentFailed;
            Log.Debug("Donation with transaction reference '{0}' set to status {1}",
                new object[] { notification.TransactionReference, donation.Status });
        }

        // write changes
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task SendEmail(DonationData donation, CancellationToken cancellationToken)
    {
        Log.Debug("Sending email to {1} for donation with transaction reference '{0}'",
            new object[] { donation.TransactionReference, donation.Donor.Email });

        // Could try to get more info from Payment Service Provider

        var email = new TemplateEmailModel(TEMPLATE_NAME)
        {
            To = donation.Donor.Email,
        };
        // convert the donation into a donation report
        email.TemplateData = ReportDonations.CreateFromDonation(donation);
        await _mediator.Publish(email, cancellationToken);
    }
}