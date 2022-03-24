using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Mediums;

public class GetMediumRequest
{
    public string Code { get; set;  }
    public MediumIdType MediumId => new(Code);
}