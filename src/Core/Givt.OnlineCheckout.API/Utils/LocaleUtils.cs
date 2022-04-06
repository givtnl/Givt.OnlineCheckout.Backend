namespace Givt.OnlineCheckout.API.Utils
{
    public static class LocaleUtils
    {
        public static string GetLocaleId(string locale, Microsoft.Extensions.Primitives.StringValues acceptLanguage, string defaultVal)
        {
            if (!String.IsNullOrWhiteSpace(locale))
                return locale;


            // Multiple types, weighted with the quality value syntax:
            // Accept-Language: fr-CH, fr;q=0.9, en;q=0.8, de;q=0.7, *;q=0.5
            var headerVal = acceptLanguage.FirstOrDefault();
            if (!String.IsNullOrWhiteSpace(locale))
            {
                var localeId = headerVal.Split(',').First()?.Trim();
                if (localeId != null)
                    return localeId.Split(';').First()?.Trim()?.Replace('_', '-');
            }

            return defaultVal;
        }
    }
}
