using System.Globalization;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Reports.Get;

public class GetDonationReportCommand : IRequest<GetDonationReportCommandResponse>
{
    public CultureInfo Culture { get; set; }
    public string TransactionReference { get; set; }
}
