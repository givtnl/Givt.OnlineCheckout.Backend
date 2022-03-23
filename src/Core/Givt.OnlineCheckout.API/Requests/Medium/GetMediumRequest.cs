using Givt.OnlineCheckout.API.Models;
using Givt.OnlineCheckout.API.Requests.Merchants;

namespace Givt.OnlineCheckout.API.Requests.Medium;

public class GetMediumRequest
{
    public string Code { get; set;  }
    public MediumIdType MediumId => System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(Code));
}