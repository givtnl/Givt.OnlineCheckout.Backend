using Givt.OnlineCheckout.Integrations.Stripe.SDK;

namespace Givt.OnlineCheckout.Integrations.Stripe
{
    public class StripeIntegration
    {
        public CustomerService CustomerService { get; }
        public StripeIntegration(CustomerService stripeCustomerService)
        {
            CustomerService = stripeCustomerService;
        }
        public async Task<Customer> CreateCustomerAsync(CustomerCreateOptions options)
        {
            return await CustomerService.CreateAsync(options);
        }
        public async Task<Customer> UpdateCustomerAsync(CustomerUpdateOptions options)
        {
            return await CustomerService.UpdateAsync(options);
        }
    }
}