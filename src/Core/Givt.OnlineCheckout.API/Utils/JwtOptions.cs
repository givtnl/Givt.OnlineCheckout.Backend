namespace Givt.OnlineCheckout.API.Utils
{
    public class JwtOptions
    {
        public const string SectionName = "JWT";
        public string Issuer { get; set; } = String.Empty; // e.g. https://api.givtapp.net
        public string IssuerSigningKey { get; set; } = String.Empty; // e.g. "21ED1753-C9BE-4BB5-A13A-710A336EE010"
        public string Audience { get; set; } = String.Empty; // e.g. https://api.givtapp.net
        public string Authority { get; set; } = String.Empty; // e.g. https://api.givtapp.net
        public int ExpireHours { get; set; } = 24;
    }
}
