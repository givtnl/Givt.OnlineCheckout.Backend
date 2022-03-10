﻿namespace Givt.OnlineCheckout.API.Example.Application
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
    }
}
