﻿namespace Givt.OnlineCheckout.Business.Reports;

public class GetDonationReportCommandResponse
{
    public string Filename { get; set; }
    public string MimeType { get; set; }
    public byte[] Content { get; set; }
}
