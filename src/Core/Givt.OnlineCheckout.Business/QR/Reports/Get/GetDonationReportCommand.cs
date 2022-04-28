using System.Globalization;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Reports.Get;

public class GetDonationReportCommand : IRequest<GetDonationReportCommandResponse>
{
    public CultureInfo CurrentCulture { get; set; }
    public string TransactionReference { get; set; }
}
