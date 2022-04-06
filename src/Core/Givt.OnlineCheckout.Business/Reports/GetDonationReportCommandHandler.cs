using AutoMapper;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.Reports;

public record GetDonationReportCommandHandler(OnlineCheckoutContext context, Mapper mapper, IPdfService pdfService) :
    IRequestHandler<GetDonationReportCommand, GetDonationReportCommandResponse>
{
    public async Task<GetDonationReportCommandResponse> Handle(GetDonationReportCommand request, CancellationToken cancellationToken)
    {
        var donation = await context.Donations
            .Include(d => d.Medium)
            .ThenInclude(m => m.Texts)
            .Include(d => d.Medium)
            .ThenInclude(m => m.Organisation)
            .ThenInclude(o => o.Texts)
            .Where(d => d.TransactionReference == request.TransactionReference)
            .FirstOrDefaultAsync(cancellationToken);
        
        var donationReport = mapper.Map<DonationReport>(donation, opt => { opt.Items["Language"] = request.Language; });
        var fileData = await pdfService.CreateSinglePaymentReport(/*donationReport, */request.Language, cancellationToken);
        return mapper.Map<GetDonationReportCommandResponse>(fileData);
    }
}