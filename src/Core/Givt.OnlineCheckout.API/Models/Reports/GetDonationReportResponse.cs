using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Reports;

public class GetDonationReportResponse
{
    public string Filename { get; set; }
    public string MimeType { get; set; }
    public byte[] Content { get; set; }
}
