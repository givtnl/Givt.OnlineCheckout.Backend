using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Givt.OnlineCheckout.Persistance.Entities;
using System.Reflection;
using integrations = Givt.OnlineCheckout.Integrations.Interfaces.Models;
using persistance = Givt.OnlineCheckout.Persistance.Enums;

namespace Givt.OnlineCheckout.Business.Extensions;

public static class MediumDataExtensions
{
    public static IEnumerable<integrations.PaymentMethod> GetPaymentMethods(this MediumData medium)
    {
        if (medium.Organisation?.PaymentMethods > 0)
            return MapPaymentMethods(medium.Organisation?.PaymentMethods);

        if (medium.Organisation?.Country?.PaymentMethods > 0)
            return MapPaymentMethods(medium.Organisation?.Country?.PaymentMethods);
        return new List<PaymentMethod>// default set: all known
        {
            PaymentMethod.Bancontact,
            PaymentMethod.Card,
            PaymentMethod.Ideal,
            PaymentMethod.Sofort,
            PaymentMethod.Giropay,
            PaymentMethod.EPS,
            PaymentMethod.ApplePay,
            PaymentMethod.GooglePay,
        };
    }

    private static IEnumerable<integrations.PaymentMethod> MapPaymentMethods(persistance.PaymentMethod? paymentMethods)
    {
        if (paymentMethods == null)
            return new List<PaymentMethod>();
        var businessPaymentMethods = (UInt64)paymentMethods.Value;
        var apiPaymentMethods = new List<integrations.PaymentMethod>();
        UInt64 mask = 0x0000000000000001;
        for (integrations.PaymentMethod i = 0; i <= (integrations.PaymentMethod)63; i++)
        {
            if ((businessPaymentMethods & mask) != 0) { apiPaymentMethods.Add(i); }
            mask <<= 1;
        }
        return apiPaymentMethods;
    }

    // Select the best matching text on locale from the medium, fall back to texts defined for the organisation
    public static string GetLocalisedText(this MediumData medium, string propertyName, string languageId)
    {
        // get the property value through reflection
        var propertyInfo = typeof(LocalisableTexts).GetProperty(propertyName);
        if (propertyInfo == null)
            return null;

        // get language from locale
        var p = languageId.IndexOf('-');
        var languageOnly = p > 0 ? languageId[..p] : languageId;

        string result;

        // match on Medium texts
        result = GetMatchingText(medium.Texts.ToList<LocalisableTexts>(), languageId, languageOnly, propertyInfo);
        if (result != null) return result;
        // match on Organisation texts
        result = GetMatchingText(medium.Organisation.Texts.ToList<LocalisableTexts>(), languageId, languageOnly, propertyInfo);
        if (result != null) return result;

        // Look for text in lingua franca
        if (!languageOnly.Equals("en", StringComparison.OrdinalIgnoreCase))
        {
            // match on Medium texts on default language "en" only
            result = GetMatchingText((ICollection<LocalisableTexts>)medium.Texts, "en", propertyInfo);
            if (result != null) return result;
            // match on Organisation texts on default language "en" only
            result = GetMatchingText((ICollection<LocalisableTexts>)medium.Organisation.Texts, "en", propertyInfo);
            if (result != null) return result;
        }

        // Still desperately seeking... match any text
        result = GetAnyText((ICollection<LocalisableTexts>)medium.Texts, propertyInfo);
        if (result != null) return result;
        result = GetAnyText((ICollection<LocalisableTexts>)medium.Organisation.Texts, propertyInfo);
        if (result != null) return result;

        return String.Empty;
    }

    private static string GetMatchingText(IList<LocalisableTexts> texts, string languageId, string languageOnly, PropertyInfo propertyInfo)
    {
        if (texts?.Count > 0)
        {
            LocalisableTexts match = null;
            // exact match on language AND region 
            match = texts
                .Where(t => t.LanguageId.Equals(languageId, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
            if (match != null && propertyInfo.GetValue(match) is string result && !String.IsNullOrEmpty(result))
                return result;
        }
        return GetMatchingText(texts, languageOnly, propertyInfo);
    }

    private static string GetMatchingText(ICollection<LocalisableTexts> texts, string language, PropertyInfo propertyInfo)
    {
        if (texts?.Count > 0)
        {
            LocalisableTexts match = null;
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

    private static string GetAnyText(ICollection<LocalisableTexts> texts, PropertyInfo propertyInfo)
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