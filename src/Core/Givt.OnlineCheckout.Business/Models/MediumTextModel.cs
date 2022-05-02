namespace Givt.OnlineCheckout.Business.Models;

public class MediumTextModel : MediumTextsCore, IConcurrency
{
    public uint ConcurrencyToken { get; set; }    
    public string LanguageId { get; set; } 
}
