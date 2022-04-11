namespace Givt.OnlineCheckout.Integrations.GoogleDocs;

public class GoogleDocsOptions
{
    public static string SectionName = "GoogleDocsOptions";
    public string DonationConfirmationNL { get; set; }
    public string DonationConfirmationEN { get; set; }
    public string PrivateKey { get; set; }
    public string ApplicationName { get; set; }
    public string ServiceAccountEmail { get; set; }
}