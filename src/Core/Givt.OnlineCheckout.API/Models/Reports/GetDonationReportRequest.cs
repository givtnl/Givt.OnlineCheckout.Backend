using System.ComponentModel;

namespace Givt.OnlineCheckout.API.Models.Reports;

public class GetDonationReportRequest
{
    [DefaultValue("en")]
    [Description("Language/Region for report")]
    public string Locale { get; set; }
    //internal string TransactionReference { get; set; }
}
