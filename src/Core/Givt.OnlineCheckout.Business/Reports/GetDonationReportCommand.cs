using MediatR;

namespace Givt.OnlineCheckout.Business.Reports;

public class GetDonationReportCommand : IRequest<GetDonationReportCommandResponse>
{
    public string Locale { get; set; }
    public string TransactionReference { get; set; }
}
