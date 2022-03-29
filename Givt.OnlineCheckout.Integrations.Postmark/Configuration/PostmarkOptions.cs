namespace Givt.OnlineCheckout.Integrations.Postmark.Configuration;

public class PostmarkOptions
{
    public PostmarkOptions() 
    {
        EnvironmentName = "Production";
    }

    public string ApiKey { get; set; }
    public string SupportAddress { get; set; }
    public string SupportName { get; set; }
    public string EnvironmentName { get; set; }
}
