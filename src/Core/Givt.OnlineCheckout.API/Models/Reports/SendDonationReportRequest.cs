namespace Givt.OnlineCheckout.API.Models.Reports;

public class SendDonationReportRequest
{
    internal string TransactionReference { get; set; }
    public string Locale { get; set; }
    public string Email { get; set; }
}
