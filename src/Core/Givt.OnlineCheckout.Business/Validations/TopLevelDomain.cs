using Serilog.Sinks.Http.Logger;

namespace Givt.OnlineCheckout.Business.Validations;

public static class TopLevelDomain
{
    private const string _url = "https://data.iana.org/TLD/tlds-alpha-by-domain.txt";
    private static HashSet<string> _tldCache;
    private static DateTime? _cacheExpiryDateTime;

    public async static Task<bool> IsValid(string tld, ILog log, CancellationToken cancellationToken)
    {
        if (_cacheExpiryDateTime == null || _cacheExpiryDateTime.Value < DateTime.UtcNow || _tldCache == null)
            await DoFetchAsync(log, cancellationToken);

        return _tldCache.Contains(tld.ToLowerInvariant());
    }


    private static async Task DoFetchAsync(ILog log, CancellationToken cancellationToken)
    {
        using (var client = new HttpClient())
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                log.Debug("Fetching list of TLDs");
                var responseBody = await client.GetStringAsync(_url, cancellationToken);
                log.Debug("Parsing TLD list");
                var data = new HashSet<string>();
                foreach (var line in responseBody.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // skip empty lines
                    if (String.IsNullOrWhiteSpace(line))
                        continue;
                    // skip comment line(s)
                    if (line.StartsWith('#'))
                        continue;
                    // skip ponycoded TLDs (non-ascii)
                    if (line.StartsWith("XN--", StringComparison.OrdinalIgnoreCase))
                        continue;
                    data.Add(line.ToLowerInvariant());
                }
                log.Information($"{data.Count} ASCII TLDs received");

                // replace current cache with newly received data and set expiry date
                _tldCache = data;
                _cacheExpiryDateTime = DateTime.UtcNow.AddDays(7);
            }
            catch (HttpRequestException hre)
            {
                log.Error(hre.ToString());
            }
        }
    }
}