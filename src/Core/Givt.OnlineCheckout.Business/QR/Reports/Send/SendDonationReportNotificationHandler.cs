using AutoMapper;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Givt.OnlineCheckout.Business.QR.Reports.Send;

public record SendDonationReportNotificationHandler(
    ILogger logger,
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

        // Google Docs expects this
        var singleDonationMessage = mapper.Map<DonationReport>(donation, opt => { opt.Items["Language"] = notification.CurrentCulture; });
        // the Postmark template expects this
        var multipleDonationMessage = mapper.Map<DonationsReport>(donation, opt => { opt.Items["Language"] = notification.CurrentCulture; });
        var fileData = await pdfService.CreateSinglePaymentReport(singleDonationMessage, notification.CurrentCulture, cancellationToken);

        var email = new BaseEmailModel
        {
            EmailType = EmailType.SingleDonation,
            To = notification.Email,
            TemplateData = multipleDonationMessage,
            Attachment = fileData.Content,
            AttachmentFileName = fileData.Filename,
            AttachmentContentType = fileData.MimeType,
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
            .ThenInclude(o => o.Texts)
            .Where(d => d.TransactionReference == transactionReference)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
