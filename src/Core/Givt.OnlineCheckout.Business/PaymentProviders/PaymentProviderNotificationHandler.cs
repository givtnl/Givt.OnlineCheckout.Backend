using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Persistance.Entities;
using Givt.OnlineCheckout.Persistance.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Serilog.Sinks.Http.Logger;
using System.Globalization;

namespace Givt.OnlineCheckout.Business.PaymentProviders;

// Setup polymorphic dispatch, this handler is now a "generic" handler for every ISinglePaymentNotification
public class PaymentProviderNotificationHandler<TPaymentNotification> : INotificationHandler<TPaymentNotification>
    where TPaymentNotification : ISinglePaymentNotification
{
    private const string TEMPLATE_NAME = "Some Template Name";
    private const string CURRENCY = "Currency";
    private const string AMOUNT = "Amount";
    private const string TRANSACTION_DATE = "TransactionDate";
    private const string TRANSACTION_REFERENCE = "TransactionReference";
    private const string TRANSACTION_STATUS = "TransactionStatus";
    private readonly ILog _log;
    private readonly OnlineCheckoutContext _context;
    private readonly IMediator _mediator;

    public PaymentProviderNotificationHandler(ILog log, OnlineCheckoutContext context, IMediator mediator)
    {
        _log = log;
        _context = context;
        _mediator = mediator;
    }

    public async Task Handle(TPaymentNotification notification, CancellationToken cancellationToken)
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

        // send confirmation if TaxRefund requested
        if (!String.IsNullOrWhiteSpace(donation.Donor?.Email)) // donation is linked to donor (this only happens if a TaxReport is requested) and email is filled in
        {
            // collect information to build confirmation email/report
            await SendEmail(donation, cancellationToken);
        }

        return;
    }

    private async Task SendEmail(DonationData donation, CancellationToken cancellationToken)
    {
        _log.Debug("Sending email to {1} for donation with transaction reference '{0}'",
            new object[] { donation.TransactionReference, donation.Donor.Email });

        var email = new TemplateEmailModel(TEMPLATE_NAME)
        {
            To = donation.Donor.Email
        };
        email.TemplateData[CURRENCY] = donation.Currency;
        email.TemplateData[AMOUNT] = donation.Amount;
        email.TemplateData[TRANSACTION_DATE] = donation.TransactionReference;
        email.TemplateData[TRANSACTION_REFERENCE] = 
            CreateLocalTimeStr("nl-NL", donation.TransactionDate, donation.TimezoneOffset);
        email.TemplateData[TRANSACTION_STATUS] = donation.Status.ToString();// TODO: localise
        // etc.
        // Could try to get more info from Payment Service Provider

        await _mediator.Publish(email, cancellationToken);
    }

    private string CreateLocalTimeStr(string locale, DateTime? transactionDate, int timezoneOffset)
    {
        var offset = new TimeSpan(0, -timezoneOffset, 0, 0);
        var culture = new CultureInfo(locale);
        var localDateTime = transactionDate.Value.Add(offset);
        var localDateTimeStr = localDateTime.ToString(culture) + offset.ToString("HH:mm");
        return localDateTimeStr;
    }

}