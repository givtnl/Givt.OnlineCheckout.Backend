using Givt.OnlineCheckout.Persistance.Entities;
using Givt.OnlineCheckout.Persistance.Enums;
using System.Reflection;

namespace Givt.OnlineCheckout.Business.Extensions;

public static class MediumDataExtensions
{
    public static IEnumerable<PaymentMethod> GetPaymentMethods(this MediumData medium)
    {
        if (medium.Organisation?.PaymentMethods?.Any() == true)
            return medium.Organisation.PaymentMethods;

        if (medium.Organisation?.Country?.PaymentMethods?.Any() == true)
            return medium.Organisation.Country.PaymentMethods;

        return new List<PaymentMethod>();
    }


    // Select the best matching text on locale from the medium, fall back to texts defined for the organisation
    public static string GetLocalisedText(this MediumData medium, string propertyName, string languageId)
    {
        if (medium == null)
            return null;
        // get the property value through reflection
        var propertyInfo = typeof(MediumTexts).GetProperty(propertyName);
        if (propertyInfo == null)
            return null;

        // get language from locale
        var p = languageId.IndexOf('-');
        var languageOnly = p > 0 ? languageId[..p] : languageId;

        string result;

        var mediumTexts = medium.Texts.ToList<MediumTexts>();
        // match on Medium texts
        result = GetMatchingText(mediumTexts, languageId, languageOnly, propertyInfo);
        if (result != null) return result;

        // Look for text in lingua franca
        if (!languageOnly.Equals("en", StringComparison.OrdinalIgnoreCase))
        {
            // match on Medium texts on default language "en" only
            result = GetMatchingText(mediumTexts, "en", propertyInfo);
            if (result != null) return result;
        }

        // Still desperately seeking... match any text
        result = GetAnyText(mediumTexts, propertyInfo);
        if (result != null) return result;

        return String.Empty;
    }

    private static string GetMatchingText(IList<MediumTexts> texts, string languageId, string languageOnly, PropertyInfo propertyInfo)
    {
        if (texts?.Count > 0)
        {
            MediumTexts match = null;
            // exact match on language AND region 
            match = texts
                .Where(t => t.LanguageId.Equals(languageId, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
            if (match != null && propertyInfo.GetValue(match) is string result && !String.IsNullOrEmpty(result))
                return result;
        }
        return GetMatchingText(texts, languageOnly, propertyInfo);
    }

    private static string GetMatchingText(ICollection<MediumTexts> texts, string language, PropertyInfo propertyInfo)
    {
        if (texts?.Count > 0)
        {
            MediumTexts match = null;
            // language only
            match = texts
                .Where(t => t.LanguageId.Equals(language, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
            if (match != null && propertyInfo.GetValue(match) is string result1 && !String.IsNullOrEmpty(result1))
                return result1;

            // other language codes with same language
            match = texts
                .Where(t => t.LanguageId.StartsWith(language + '-', StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
            if (match != null && propertyInfo.GetValue(match) is string result2 && !String.IsNullOrEmpty(result2))
                return result2;
        }
        return null;
    }

    private static string GetAnyText(ICollection<MediumTexts> texts, PropertyInfo propertyInfo)
    {
        if (texts?.Count > 0)
        {
            foreach (var match in texts)
                if (propertyInfo.GetValue(match) is string result && !String.IsNullOrWhiteSpace(result))
                    return result;
        }
        return null;
    }

}