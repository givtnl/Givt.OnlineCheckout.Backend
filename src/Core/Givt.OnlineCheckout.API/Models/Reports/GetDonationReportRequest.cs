using System.ComponentModel;
using System.Globalization;

namespace Givt.OnlineCheckout.API.Models.Reports;

public class GetDonationReportRequest
{    
    [Description("Language/Region for texts. Defaults to AcceptLanguage, otherwise 'en'")]
    public CultureInfo CurrentCulture { get; set; }
}
