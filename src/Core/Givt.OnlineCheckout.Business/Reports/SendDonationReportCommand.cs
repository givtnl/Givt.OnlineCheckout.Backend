using MediatR;

namespace Givt.OnlineCheckout.Business.Reports
{
    public class SendDonationReportCommand: IRequest<SendDonationReportCommandResponse>
    {
        public string TransactionId { get; set; }
        public string Email { get; set; }
    }
}
