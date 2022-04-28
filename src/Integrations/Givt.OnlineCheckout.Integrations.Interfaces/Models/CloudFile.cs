namespace Givt.OnlineCheckout.Integrations.Interfaces.Models;

public class CloudFile
{
    public long Size { get; set; }
    public string ContentType { get; set; }
    public byte[] Contents { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
}