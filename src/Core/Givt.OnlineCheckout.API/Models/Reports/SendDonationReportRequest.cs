using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Givt.OnlineCheckout.API.Models.Reports;

public class SendDonationReportRequest
{
    [DefaultValue("en")]
    [Description("Language/Region for texts")]
    public string Locale { get; set; }

    [Required()]
    [Description("Email address to send the report to")]
    public string Email { get; set; }
}
