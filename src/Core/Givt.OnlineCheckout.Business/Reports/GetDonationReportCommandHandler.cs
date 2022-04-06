using AutoMapper;
using Givt.OnlineCheckout.Business.Models.Report;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
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
        var donationData = _mapper.Map<SingleDonationReport>( ReportDonations.CreateFromDonation(donation, request.Language));
        var fileData = await pdfService.CreateSinglePaymentReport(
            donationData,
            request.Language,
            cancellationToken);        

        return _mapper.Map<GetDonationReportCommandResponse>(fileData);
    }
}