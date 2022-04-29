namespace Givt.OnlineCheckout.Business.Models;

public class LocalisableTextModel : LocalisableTextsCore, IConcurrency
{
    public uint ConcurrencyToken { get; set; }    
    public string LanguageId { get; set; } 
}
