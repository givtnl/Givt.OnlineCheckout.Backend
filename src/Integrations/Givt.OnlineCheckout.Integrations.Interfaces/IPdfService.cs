namespace Givt.OnlineCheckout.Integrations.Interfaces;

public interface IPdfService
{
   Task<IFileData> CreateSinglePaymentReport(string locale, CancellationToken cancellationToken);
}

public interface IFileData
{
    public byte[] Content { get; set; }
    public string Filename { get; set; }
    public string MimeType { get; set; }
}