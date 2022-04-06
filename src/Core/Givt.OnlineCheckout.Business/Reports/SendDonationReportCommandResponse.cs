namespace Givt.OnlineCheckout.Business.Reports
{
    public class SendDonationReportCommandResponse
    {
        public string Filename { get; set; }
        public string MimeType { get; set; } // e.g. application/pdf
        public byte[] Content { get; set; }
    }
}