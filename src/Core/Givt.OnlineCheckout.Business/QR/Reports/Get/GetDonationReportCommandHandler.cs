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
        // TODO: if required, find/create DonorData by email, and link to this donation
        var culture = new CultureInfo(donation.Medium.Organisation.Country.Locale);
        Thread.CurrentThread.CurrentCulture = culture;  
        var donationReport = mapper.Map<DonationReport>(donation, opt => { opt.Items[DonationReportMappingProfile.LanguageTag] = culture; });
        var fileData = await pdfService.CreateSinglePaymentReport(donationReport, culture, cancellationToken);
        return mapper.Map<GetDonationReportCommandResponse>(fileData);
    }
}