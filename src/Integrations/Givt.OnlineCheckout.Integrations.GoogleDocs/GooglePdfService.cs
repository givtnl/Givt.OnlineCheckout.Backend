using System.Globalization;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Docs.v1;
using Google.Apis.Docs.v1.Data;
using Google.Apis.Drive.v3;
using Google.Apis.Http;
using Google.Apis.Services;
using File = Google.Apis.Drive.v3.Data.File;

namespace Givt.OnlineCheckout.Integrations.GoogleDocs;

public class GooglePdfService: IPdfService
{
    private readonly GoogleDocsOptions _options;
    private readonly string[] _userScopes = { DocsService.Scope.Drive, DocsService.Scope.Documents };

    public GooglePdfService(GoogleDocsOptions options)
    {
        _options = options;
    }
    
    
    public async Task<IFileData> CreateSinglePaymentReport(SingleDonationReport report, string locale, CancellationToken cancellationToken)
    {
        var parameters = new Dictionary<string, string>();
        parameters.Add("receivingOrganisation", report.OrganisationName);
        parameters.Add("date", report.Date.ToString("o"));
        parameters.Add("currencySymbol", "€");
        parameters.Add("amount", report.Amount.ToString(CultureInfo.InvariantCulture));
        var document = await GenerateDocument(parameters , "1L-5CEuy7df4TO77LVzl13VzEnQXtOTRO4imHM5TTf2E", cancellationToken);
        return new GoogleFile()
        {
            Content = document,
            Filename = "receipt.pdf",
            MimeType = "application/pdf"
        };
    }

    private async Task FillInAFile(string fileId, Dictionary<string, string> parameters, CancellationToken token)
    {
        if (!parameters.Any())
            return;
        // Create Google Docs API service.
        var service = BuildDocsService();

        // Fill in the file
        var requests = new List<Request>();

        foreach (var parameter in parameters)
        {
            var req = new Request();
            var replaceAllTextRequest = new ReplaceAllTextRequest();
            var criteria = new SubstringMatchCriteria { Text = $"{{{parameter.Key}}}" };
            replaceAllTextRequest.ContainsText = criteria;
            replaceAllTextRequest.ReplaceText = parameter.Value;
            req.ReplaceAllText = replaceAllTextRequest;
            requests.Add(req);
        }

        var body = new BatchUpdateDocumentRequest { Requests = requests };


        var request = service.Documents.BatchUpdate(body, fileId);
        await request.ExecuteAsync(token);
    }
    
    private IConfigurableHttpClientInitializer LoadCredentials()
    {
        return new ServiceAccountCredential(
            new ServiceAccountCredential.Initializer(_options.ServiceAccountEmail)
            {
                Scopes = _userScopes
            }.FromPrivateKey(_options.PrivateKey));
    }

    private DocsService BuildDocsService()
    {
        return new DocsService(BuildInitializer());
    }
    
    private DriveService BuildDriveService()
    {
        return new DriveService(BuildInitializer());
    }
    
    private BaseClientService.Initializer BuildInitializer()
    {
        // create credentials
        return new BaseClientService.Initializer()
        {
            HttpClientInitializer = LoadCredentials(),
            ApplicationName = _options.ApplicationName,
        };
    }

    private async Task<byte[]> GenerateDocument(Dictionary<string, string> parameters, string documentId, CancellationToken token)
    {
        var newFileId = await CopyFile(documentId, $"tempDoc_{DateTime.UtcNow:HH:mm:ss.fff}", token);
        byte[] documentContents = null;
        try
        {
            await FillInAFile(newFileId, parameters, token);

            documentContents = DownloadFile(newFileId);
        }
        catch (Exception)
        {
            // retry ofzo
        }
        finally
        {
            await DeleteFile(newFileId, token);
        }


        return documentContents;
    }

    private async Task<string> CopyFile(string fileId, string newFileName, CancellationToken token)
    {
        // Copy the file
        var file = new File { Name = newFileName };

        var copyRequest = BuildDriveService().Files.Copy(file, fileId);
        var newFile = await copyRequest.ExecuteAsync(token);

        return newFile.Id;
    }

    private async Task DeleteFile(string newFileId, CancellationToken token)
    {
        var deleteRequest = BuildDriveService().Files.Delete(newFileId);
        await deleteRequest.ExecuteAsync(token);
    }

    private byte[] DownloadFile(string fileId)
    {
        using var stream = new MemoryStream();
        var request = BuildDriveService().Files.Export(fileId, "application/pdf");
        request.DownloadWithStatus(stream);
        return stream.ToArray();
    }
    
}