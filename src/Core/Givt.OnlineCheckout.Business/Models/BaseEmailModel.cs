using Givt.OnlineCheckout.Integrations.Interfaces;

namespace Givt.OnlineCheckout.Business.Models
{
    public class BaseEmailModel : IEmailNotification
    {
        public EmailType EmailType { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Tag { get; set; }
        public object TemplateData { get; set; }
        public string HtmlBody { get; set; }
        public string ReplyTo { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public bool TrackOpens { get; set; }
        public string TrackLinks { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public byte[] Attachment { get; set; }
        public string AttachmentFileName { get; set; }
        public string AttachmentContentType { get; set; }
        public List<string> AttachmentFiles { get; set; }
        public string Locale { get; set; }
    }
}
