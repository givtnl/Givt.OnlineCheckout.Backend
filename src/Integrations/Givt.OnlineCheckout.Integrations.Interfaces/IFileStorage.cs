using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.Integrations.Interfaces;

public interface IFileStorage
{
    Task<CloudFile> DownloadFile(string containerName, string fileName, CancellationToken token);
    Task UploadFile(string containerName, string path, Stream file, string contentType, CancellationToken cancellationToken = default(CancellationToken));
    Task<bool> FileExists(string containerName, string path, CancellationToken cancellationToken);
    Task DeleteFile(string containerName, string path, CancellationToken cancellationToken);
}