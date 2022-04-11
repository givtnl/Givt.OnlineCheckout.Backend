using Google.Apis.Docs.v1;
using Google.Apis.Docs.v1.Data;

namespace Givt.OnlineCheckout.Integrations.GoogleDocs;

public class GoogleDocsService: BaseGoogleService<DocsService>
{
    public GoogleDocsService(GoogleDocsOptions options): base(options) { }

    protected override DocsService BuildService()
    {
        return new DocsService(BuildInitializer());
    }

    internal async Task FillInAFile(string fileId, Dictionary<string, string> parameters, CancellationToken token)
    {
        if (!parameters.Any())
            return;
        // Create Google Docs API service.
        var service = BuildService();

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
}