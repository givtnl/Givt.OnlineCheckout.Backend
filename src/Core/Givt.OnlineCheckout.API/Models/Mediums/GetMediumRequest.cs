using Givt.OnlineCheckout.API.Models.Organisations;
using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Mediums;

public class GetMediumRequest
{
    public MediumIdType MediumId { get; set; }
    public static bool TryParse(string value, out GetOrganisationRequest request)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException($"'{nameof(value)}' cannot be null or empty.", nameof(value));
        }
        request = new GetOrganisationRequest() { MediumId = value };
        return true;
    }
}