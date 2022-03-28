using Serilog.Sinks.Http.Logger;
using System.Text.RegularExpressions;

namespace Givt.OnlineCheckout.Business.Validations
{
    public static class EmailAddress
    {
        /// <summary>
        /// Check if an email address is valid
        /// </summary>
        /// <param name="email">Email address to check</param>
        /// <returns>null = OK, otherwise a message stating the problem</returns>
        public static string IsValid(string email, ILog log)
        {

            // check non-empty
            if (String.IsNullOrWhiteSpace(email))
                return "Empty email address";

            // check length
            if (email.Length > 70)
                return "Email address is too long (max 70 characters allowed)";

            // check  e-mail format
            var p = email.LastIndexOf('@');
            if (p == -1)
                return "Email is not correctly formatted (@ is missing)";
            //var localPart = email[..p]; // may offically contain spaces, @ and end with a '.' etc. In reality local parts "nearly always" are far more limited.
            //var domain = email[(p + 1)..]; // may officially be a FQDN or an IP6 or IP6 address
            //we assume mailbox@[sub.]domain.tld

            try
            {
                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                    return "Email is not correctly formatted";
            }
            catch (RegexMatchTimeoutException)
            {
                return "Internal error, checking email format took too long";
            }
            // check TLD
            p = email.LastIndexOf('.');
            var tld = email[(p + 1)..];
            if (!TopLevelDomain.IsValid(tld, log))
                return "Top level domain is unknown";

            return null;
        }
    }
}
