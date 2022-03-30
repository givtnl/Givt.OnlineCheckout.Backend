namespace Givt.OnlineCheckout.Integrations.Postmark.Models;

public class PostmarkEmailBody
{
    public string From { get; set; }
    public string To { get; set; }
    public string Cc { get; set; }
    public string Bcc { get; set; }
    public string Subject { get; set; }
    public string Tag { get; set; }
    public string HtmlBody { get; set; }
    public string TextBody { get; set; }
    public string ReplyTo { get; set; }
    public Dictionary<string, string> Headers { get; set; }
    public bool TrackOpens { get; set; }
    public string TrackLinks { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
    public List<string> Attachments { get; set; }
}
