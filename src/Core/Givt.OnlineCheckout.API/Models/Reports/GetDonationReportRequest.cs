namespace Givt.OnlineCheckout.API.Models.Reports;

public class GetDonationReportRequest
{
    internal string TransactionReference { get; set; }
    public string Locale { get; set; }
}
