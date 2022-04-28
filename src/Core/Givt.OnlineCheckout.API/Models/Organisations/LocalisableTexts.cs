using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Organisations;

public class LocalisableTextsInfo : LocalisableTextsInfoCore, IConcurrency
{
    public uint ConcurrencyToken { get; set; }
    public string LanguageId { get; set; }
}