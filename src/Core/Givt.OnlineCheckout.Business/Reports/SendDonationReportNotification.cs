using MediatR;

namespace Givt.OnlineCheckout.Business.Reports
{
    public class SendDonationReportNotification: INotification
    {
        public string Language { get; set; }
        public string TransactionReference { get; set; }
        public string Email { get; set; }
    }
}
