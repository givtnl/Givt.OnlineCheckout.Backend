using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Organisations;

public class MediumTextsInfo : MediumTextsInfoCore, IConcurrency
{
    public uint ConcurrencyToken { get; set; }
    public string LanguageId { get; set; }
}