using System;

namespace Givt.OnlineCheckout.API.Requests.Merchants
{
    public class GetMerchantRequest
    {
        public string MediumId { get; set; }
        public static bool TryParse(string value, out GetMerchantRequest request)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"'{nameof(value)}' cannot be null or empty.", nameof(value));
            }
            request = new GetMerchantRequest() { MediumId = value };
            return true;
        }
    }

    public class GetMerchantResponse
    {
        public string Name { get; set; }
        public decimal[] Amounts { get; set; }
        public string ThankYou { get; set; }
        public string Goal { get; set; }
    }
}
