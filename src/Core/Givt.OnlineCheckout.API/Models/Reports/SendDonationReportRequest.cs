namespace Givt.OnlineCheckout.API.Models.Reports;

public class SendDonationReportRequest
{
    public string Locale { get; set; }
    public string Email { get; set; }
    internal string TransactionReference { get; set; }
}
