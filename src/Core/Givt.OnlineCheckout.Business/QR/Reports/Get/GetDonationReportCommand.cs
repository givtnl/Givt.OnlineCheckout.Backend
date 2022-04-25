using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Reports.Get;

public class GetDonationReportCommand : IRequest<GetDonationReportCommandResponse>
{
    public string Language { get; set; }
    public string TransactionReference { get; set; }
}
