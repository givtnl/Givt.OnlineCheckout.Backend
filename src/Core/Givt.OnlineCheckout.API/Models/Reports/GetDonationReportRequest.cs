namespace Givt.OnlineCheckout.API.Models.Reports;

public class GetDonationReportRequest
{
    public string Locale { get; set; }
    internal string TransactionReference { get; set; }
}
