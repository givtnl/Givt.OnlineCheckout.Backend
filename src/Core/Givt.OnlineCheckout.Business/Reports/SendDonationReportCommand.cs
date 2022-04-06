using MediatR;

namespace Givt.OnlineCheckout.Business.Reports
{
    public class SendDonationReportCommand: INotification
    {
        public string Language { get; set; }
        public string TransactionReference { get; set; }
        public string Email { get; set; }
    }
}
