using Givt.OnlineCheckout.Integrations.Interfaces;

namespace Givt.OnlineCheckout.Integrations.GoogleDocs;

public class GoogleFile: IFileData
{
    public byte[] Content { get; set; }
    public string Filename { get; set; }
    public string MimeType { get; set; }
}