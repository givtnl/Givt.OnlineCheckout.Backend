namespace Givt.OnlineCheckout.Integrations.Postmark;

public class PostmarkOptions
{
    public static string SectionName = "PostmarkOptions";

    public string ApiKey { get; set; }
    public string SupportAddress { get; set; }
    public string SupportName { get; set; }
    public string EnvironmentName { get; set; } = "Production";

    public string MailReportSingleDonationTemplate { get; set; }
}
