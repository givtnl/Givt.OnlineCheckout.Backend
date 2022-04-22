using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Reports.Send;

public class SendDonationReportNotification: INotification
{
    public string Language { get; set; }
    public string TransactionReference { get; set; }
    public string Email { get; set; }
}
