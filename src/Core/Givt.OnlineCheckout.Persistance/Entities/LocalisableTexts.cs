namespace Givt.OnlineCheckout.Persistance.Entities;

public class LocalisableTexts
{
    public uint ConcurrencyToken { get; set; }
    public string LanguageId { get; set; } // length at least 14 (ca-ES-valencia = catalan spain valencia dialect)
    public string Goal { get; set; }
    public string ThankYou { get; set; }
}