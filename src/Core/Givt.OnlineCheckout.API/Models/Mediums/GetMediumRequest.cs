using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Givt.OnlineCheckout.API.Models.Mediums;

public class GetMediumRequest
{
    [Required]
    [Description("Medium ID or Code")]
    public string Code { get; set; }

    [DefaultValue("en")]
    [Description("Language/Region for texts")]
    public string Locale { get; set; }
}