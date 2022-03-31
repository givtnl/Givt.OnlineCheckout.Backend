using MediatR;

namespace Givt.OnlineCheckout.Integrations.Interfaces
{
    public interface IEmailNotification : INotification
    {
        string From { get; }
        string To { get; }
        string Cc { get; }
        string Bcc { get; }
        string Subject { get; }
        string Tag { get; }
        string TemplateName { get; }
        object TemplateData { get; }
        string HtmlBody { get; }
        string ReplyTo { get; }
        Dictionary<string, string> Headers { get; }
        bool TrackOpens { get; }
        string TrackLinks { get; }
        Dictionary<string, string> Metadata { get; }
        byte[] Attachment { get; }
        string AttachmentFileName { get; }
        List<string> AttachmentFiles { get; }
    }

}
