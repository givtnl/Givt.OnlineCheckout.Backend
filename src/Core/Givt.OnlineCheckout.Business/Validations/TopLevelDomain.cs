using Serilog.Sinks.Http.Logger;
using System.Diagnostics;

namespace Givt.OnlineCheckout.Business.Validations;

public static class TopLevelDomain
{
    private const string _url = "https://data.iana.org/TLD/tlds-alpha-by-domain.txt";

    private static readonly object _locker = new();
    private static HashSet<string> _cache;
    private static DateTime? _cacheExpiryDateTime;

    public static bool IsValid(string tld, ILog log)
    {
        var fetcherTask = Task.CompletedTask;
        // need to support multithreading without too much locking. If we're updating the cache in high volume transactions, they should just use the cache
        lock (_locker)
        {
            if (_cacheExpiryDateTime == null || _cacheExpiryDateTime.Value < DateTime.UtcNow || _cache == null)
            {
                fetcherTask = Task.Run(() => DoFetch(log, CancellationToken.None));
            }
        }
        if (_cache == null)
        {
            // first time: everyone must wait until the cache is loaded
            fetcherTask.Wait();
        }

        if (_cache == null)
        {
            // loading TLDs into cache failed miserably
            log.Warning($"No list of TLDs (yet). Assuming TLD '{tld}' is valid");
            return true; // assume valid as we did not manage to fetch list
        }
        return _cache.Contains(tld.ToLowerInvariant());
    }


    private static async Task DoFetch(ILog log, CancellationToken cancellationToken)
    {
        using var client = new HttpClient();
        // Call asynchronous network methods in a try/catch block to handle exceptions.
        try
        {
            log.Debug("Fetching list of TLDs...");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var responseBody = await client.GetStringAsync(_url, cancellationToken);
            stopwatch.Stop();
            log.Debug($"Fetching list took {stopwatch.ElapsedMilliseconds} ms");
            log.Debug("Parsing list of TLDs");
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
            _cache = data;
            _cacheExpiryDateTime = DateTime.UtcNow.AddDays(7);
        }
        catch (HttpRequestException hre)
        {
            log.Error(hre.ToString());
        }
    }
}