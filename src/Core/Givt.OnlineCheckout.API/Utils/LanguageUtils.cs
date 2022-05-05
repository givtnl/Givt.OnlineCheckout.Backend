using Microsoft.Extensions.Primitives;

namespace Givt.OnlineCheckout.API.Utils;

public static class LanguageUtils
{
    public static string GetLanguageId(string overrideVal, StringValues acceptLanguage, string defaultVal)
    {
        /* Routine to get a language from the API front, and normalise it to a IETF standard value
         * IETF BCP 47 language tag: en-GB (https://en.wikipedia.org/wiki/IETF_language_tag
         * Http Headers a.o.: en_GB https://en.wikipedia.org/wiki/Locale_(computer_software)
         */
        if (!String.IsNullOrWhiteSpace(overrideVal))
            return overrideVal.Trim().Replace('_', '-');


        // Multiple types, weighted with the quality value syntax:
        // Accept-Language: fr-CH, fr;q=0.9, en;q=0.8, de;q=0.7, *;q=0.5        
        var headerVal = acceptLanguage.FirstOrDefault();
        if (!String.IsNullOrWhiteSpace(overrideVal))
        {
            var localeId = headerVal.Split(',').First()?.Trim();
            if (localeId != null)
                return localeId.Split(';').First()?.Trim()?.Replace('_', '-');
        }

        return defaultVal;
    }
}
