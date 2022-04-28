﻿using System.Globalization;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.Integrations.GoogleDocs;

public class GooglePdfService : IPdfService
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


    public async Task<IFileData> CreateSinglePaymentReport(DonationReport report, CultureInfo cultureInfo, CancellationToken cancellationToken)
    {
        var currSymbol = GetCurrencySymbol(report.Currency);

        var parameters = new Dictionary<string, string>
        {
            {"OrganisationName", report.OrganisationName},
            {"DateGenerated", report.Timestamp.ToString(cultureInfo)},
            {"DonationAmount", $"{currSymbol}{report.Amount.ToString("N2" ,cultureInfo)}"},
            {"RSIN", report.RSIN 
            },
            {"hmrcReference", 
                String.IsNullOrEmpty(report.HmrcReference ) ?
                null :
                "HMRC Reference: " + report.HmrcReference + " " 
            },
            {"charityNumber",
                String.IsNullOrEmpty(report.CharityNumber) ?
                null :
                "Charity Number: " + report.CharityNumber + " "
            },
            {"PaymentMethod", report.PaymentMethod},
            {"CampaignName", report.Goal},
            {"CampaignThankYouSentence", report.ThankYou},
        };
        // Now we only have english and netherlands without country, so I split on dash and take first element which is the language
        // I do this also for the name of the attachment
        var language = cultureInfo.TwoLetterISOLanguageName;
        var templateId = language switch
        {
            "nl" => _options.DonationConfirmationNL,
            _ => _options.DonationConfirmationEN
        };
        var attachmentName = language switch
        {
            "nl" => "ontvangstbewijs.pdf",
            _ => "receipt.pdf"
        };
        var document = await GenerateDocument(parameters, templateId, cancellationToken);
        return new GoogleFile()
        {
            Content = document,
            Filename = attachmentName,
            MimeType = "application/pdf"
        };
    }

    private string GetCurrencySymbol(string ISOCurrencySymbol)
    {
        return CultureInfo
            .GetCultures(CultureTypes.AllCultures)
            .Where(c => !c.IsNeutralCulture)
            .Select(culture => {
                try{
                    return new RegionInfo(culture.Name);
                }
                catch
                {
                    return null;
                }
            })
            .Where(ri => ri!=null && ri.ISOCurrencySymbol == ISOCurrencySymbol.ToUpper())
            .Select(ri => ri.CurrencySymbol)
            .FirstOrDefault();
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