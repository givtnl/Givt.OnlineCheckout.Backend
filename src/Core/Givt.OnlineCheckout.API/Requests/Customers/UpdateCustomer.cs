using System;

namespace Givt.OnlineCheckout.API.Requests.Customers
{
    public class UpdateCustomerRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdateCustomerResponse
    {
        public Guid Id { get; set; }
        public string StripeCustomerReference { get; set; }
    }

}
