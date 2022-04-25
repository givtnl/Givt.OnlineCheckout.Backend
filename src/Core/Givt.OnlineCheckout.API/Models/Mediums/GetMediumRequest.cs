using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Givt.OnlineCheckout.API.Models.Mediums;

public class GetMediumRequest
{
    [Required]
    [Description("Medium ID or Code")]
    public string Code { get; set; }
        
    [Description("Language/Region for texts. Defaults to AcceptLanguage, otherwise 'en'")]
    public string Locale { get; set; }
}