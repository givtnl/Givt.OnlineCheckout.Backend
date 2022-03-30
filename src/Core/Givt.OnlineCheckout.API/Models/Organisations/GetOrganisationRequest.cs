using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Organisations
{
    public class GetOrganisationRequest
    {
        public MediumIdType MediumId { get; set; }
        public string Locale { get; set; }

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

    public class GetOrganisationResponse
    {
       
    }
}
