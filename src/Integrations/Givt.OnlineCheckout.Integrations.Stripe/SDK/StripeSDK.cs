using System;
using System.Threading.Tasks;

namespace Givt.OnlineCheckout.Integrations.Stripe.SDK
{
    /// <summary>
    ///  Mock van Stripe SDK for .NET
    /// </summary>
    public class Customer { public string StripeCustomerReference { get; set; } }
    public class CustomerCreateOptions { public string Name { get; set; } }
    public class CustomerUpdateOptions { public Guid Id { get; set; } public string Name { get; set; } }
    public class CustomerService
    {
        public Task<Customer> CreateAsync(CustomerCreateOptions options)
        {
            return Task.FromResult(new Customer { StripeCustomerReference = $"Some sexy stripe reference from {options.Name}" });
        }
        public Task<Customer> UpdateAsync(CustomerUpdateOptions options)
        {
            Console.WriteLine(options); // So no complaint from compiler
            return Task.FromResult(new Customer { StripeCustomerReference = $"Some very sexy stripe reference from {options.Name}" });
        }
    }
}