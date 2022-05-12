using AutoMapper;
using Givt.OnlineCheckout.Business.Mappings;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Givt.OnlineCheckout.Business.QR.Reports.Get;

public record GetDonationReportCommandHandler(OnlineCheckoutContext context, IMapper mapper, IPdfService pdfService) :
    IRequestHandler<GetDonationReportCommand, GetDonationReportCommandResponse>
{
    public async Task<GetDonationReportCommandResponse> Handle(GetDonationReportCommand request, CancellationToken cancellationToken)
    {
        var donation = await context.Donations
            .Include(d => d.Medium)
            .ThenInclude(m => m.Texts)
            .Include(d => d.Medium)
            .ThenInclude(m => m.Organisation)
            .ThenInclude(o => o.Country)
            .Where(d => d.TransactionReference == request.TransactionReference)
            .FirstOrDefaultAsync(cancellationToken);

        // map data using the organisation's culture
        CultureInfo culture;
        try { culture = CultureInfo.GetCultureInfo(donation.Medium.Organisation.Country.Locale); }
        catch { culture = CultureInfo.GetCultureInfo("en-GB"); }
        Thread.CurrentThread.CurrentCulture = culture;

        var donationReport = mapper.Map<DonationReport>(donation, opt => { opt.Items[DonationReportMappingProfile.CultureTag] = culture; });
        var fileData = await pdfService.CreateSinglePaymentReport(donationReport, culture, cancellationToken);
        return mapper.Map<GetDonationReportCommandResponse>(fileData);
    }
}