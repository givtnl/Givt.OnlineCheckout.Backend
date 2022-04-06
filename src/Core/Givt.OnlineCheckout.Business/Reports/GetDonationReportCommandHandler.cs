using AutoMapper;
using Givt.OnlineCheckout.Business.Models.Report;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.Reports;

public record GetDonationReportCommandHandler(OnlineCheckoutContext context, Mapper _mapper, IPdfService pdfService) : 
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
        var donationData = ReportDonations.CreateFromDonation(donation, request.Locale);
        var fileData = await pdfService.CreateSinglePaymentReport(
            request.Locale,
            cancellationToken);        

        return _mapper.Map<GetDonationReportCommandResponse>(fileData);
    }
}