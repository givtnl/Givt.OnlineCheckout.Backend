using System.Globalization;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.Integrations.GoogleDocs;

public class GooglePdfService: IPdfService
{
    private readonly GoogleDocsOptions _options;
    private readonly GoogleDocsService _docsService;
    private readonly GoogleDriveService _driveService;

    public GooglePdfService(GoogleDocsOptions options)
    {
        _options = options;
        _docsService = new GoogleDocsService(options);
        _driveService = new GoogleDriveService(options);
    }
    
    
    public async Task<IFileData> CreateSinglePaymentReport(DonationReport report, string locale, CancellationToken cancellationToken)
    {
        var parameters = new Dictionary<string, string>
        {
            {"receivingOrganisation", report.OrganisationName},
            {"date", report.Timestamp},
            {"currencySymbol", report.Currency},
            {"amount", report.Amount.ToString(CultureInfo.InvariantCulture)}
        };
        // Now we only have english and netherlands without country, so I split on dash and take first element which is the language
        // I do this also for the name of the attachment
        var language = locale.Split('-').First();
        var templateId = language switch
        {
            "nl" => _options.DonationConfirmationNL,
            _ => _options.DonationConfirmationNL
        };
        var attachmentName = language switch
        {
            "nl" => "ontvangstbewijs.pdf",
            _ => "receipt.pdf"
        };
        var document = await GenerateDocument(parameters , templateId, cancellationToken);
        return new GoogleFile()
        {
            Content = document,
            Filename = attachmentName,
            MimeType = "application/pdf"
        };
    }

    private async Task<byte[]> GenerateDocument(Dictionary<string, string> parameters, string documentId, CancellationToken token)
    {
        // Firstly we copy the template into a copy of the template
        var newFileId = await _driveService.CopyFile(documentId, $"tempDoc_{DateTime.UtcNow:HH:mm:ss.fff}", token);
        byte[] documentContents;
        try
        {
            // We fill in the copy so we have a filled document
            await _docsService.FillInAFile(newFileId, parameters, token);
            
            documentContents = _driveService.DownloadFile(newFileId);
        }
        catch (Exception)
        {
            return null;
        }
        finally
        {
            // To be sure the Drive doesn't get cluttered we remove the copied and filled document
            await _driveService.DeleteFile(newFileId, token);
        }


        return documentContents;
    }

    
    
}