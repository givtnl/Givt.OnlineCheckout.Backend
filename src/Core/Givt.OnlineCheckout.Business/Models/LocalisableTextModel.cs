namespace Givt.OnlineCheckout.Business.Models;

public class LocalisableTextModel: LocalisableTextsCore
{
    public uint ConcurrencyToken { get; set; }
    public string LanguageId { get; set; } 
}
