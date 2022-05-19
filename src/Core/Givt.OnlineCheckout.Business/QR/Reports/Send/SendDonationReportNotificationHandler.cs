using AutoMapper;
using Givt.OnlineCheckout.Business.Mappings;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog.Sinks.Http.Logger;
using System.Globalization;

namespace Givt.OnlineCheckout.Business.QR.Reports.Send;

public record SendDonationReportNotificationHandler(
    ILog logger,
    OnlineCheckoutContext context,
    IMapper mapper,
    IMediator mediator,
    IPdfService pdfService
    ) :
    INotificationHandler<SendDonationReportNotification>
{
    public async Task Handle(SendDonationReportNotification notification, CancellationToken cancellationToken)
    {
        logger.Debug("Sending email to {1} for donation with transaction reference '{0}'",
                new object[] { notification.TransactionReference, notification.Email });
        var donation = await FetchDonation(notification.TransactionReference, cancellationToken);

        // map data using the organisation's culture
        CultureInfo culture;
        try { culture = CultureInfo.GetCultureInfo(donation.Medium.Organisation.Country.Locale); }
        catch { culture = CultureInfo.GetCultureInfo("en-GB"); }
        Thread.CurrentThread.CurrentCulture = culture;

        // Google Docs expects this
        var singleDonationMessage = mapper.Map<DonationReport>(donation, opt => { opt.Items[DonationReportMappingProfile.CultureTag] = culture; });
        // the Postmark template expects this
        var multipleDonationMessage = mapper.Map<DonationsReport>(donation, opt => { opt.Items[DonationReportMappingProfile.CultureTag] = culture; });

        var fileData = await pdfService.CreateSinglePaymentReport(singleDonationMessage, culture, cancellationToken);

        var email = new BaseEmailModel
        {
            EmailType = EmailType.SingleDonation,
            To = notification.Email,
            TemplateData = multipleDonationMessage,
            Attachment = fileData.Content,
            AttachmentFileName = fileData.Filename,
            AttachmentContentType = fileData.MimeType,
            Locale = donation.Medium.Organisation.Country.Locale
        };

        await mediator.Publish(email, cancellationToken);
    }

    private async Task<DonationData> FetchDonation(string transactionReference, CancellationToken cancellationToken)
    {
        return await context.Donations
            .Include(d => d.Medium)
            .ThenInclude(m => m.Texts)
            .Include(d => d.Medium)
            .ThenInclude(m => m.Organisation)
            .ThenInclude(o => o.Country)
            .Where(d => d.TransactionReference == transactionReference)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
