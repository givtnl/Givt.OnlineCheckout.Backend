using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Microsoft.Extensions.Options;

namespace Givt.OnlineCheckout.Integrations.AzureFileStorage;

public class AzureFileStorage : IFileStorage
{
    private readonly BlobServiceClient _client;
    public AzureFileStorage(IOptions<AzureBlobStorageOptions> settings)
    {
        _client = new BlobServiceClient(settings.Value.ConnectionString);
    }

    public async Task<CloudFile> DownloadFile(string containerName, string fileName, CancellationToken token)
    {
        var containerClient = _client.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        var response = await blobClient.DownloadAsync(token);

        using (var stream = response.Value.Content)
        using (var ms = new MemoryStream())
        {
            await stream.CopyToAsync(ms);

            return new CloudFile
            {
                ContentType = response.Value.ContentType,
                Contents = ms.ToArray(),
                Name = blobClient.Name,
                Link = blobClient.Uri.ToString(),
                Size = response.Value.ContentLength
            };
        }
    }

    public async Task<bool> FileExists(string containerName, string path, CancellationToken cancellationToken)
    {
        var blobClient = _client.GetBlobContainerClient(containerName).GetBlobClient(path);
        return (await blobClient.ExistsAsync(cancellationToken)).Value;
    }

    public async Task UploadFile(string containerName, string path, Stream file, string contentType, CancellationToken cancellationToken = default(CancellationToken))
    {
        var containerClient = _client.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(path);
        if (string.IsNullOrWhiteSpace(contentType))
            await blobClient.UploadAsync(file, true, cancellationToken);
        else {
            var blobHeaders = new BlobHttpHeaders();
            blobHeaders.ContentType = contentType;
            await blobClient.UploadAsync(file, blobHeaders, null, null, null, null, default(StorageTransferOptions), cancellationToken);
        }
    }

    public async Task DeleteFile(string containerName, string path, CancellationToken cancellationToken)
    {
        var containerClient = _client.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(path);
        await blobClient.DeleteAsync(DeleteSnapshotsOption.IncludeSnapshots, null, cancellationToken);
    }
}