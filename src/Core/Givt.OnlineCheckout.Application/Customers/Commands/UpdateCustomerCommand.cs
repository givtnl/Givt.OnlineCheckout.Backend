using AutoMapper;
using Givt.OnlineCheckout.Application.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Stripe;
using Givt.OnlineCheckout.Integrations.Stripe.SDK;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Application.Customers.Commands
{

    public class UpdateCustomerCommand : IRequest<CustomerDetailModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDetailModel>
    {
        public IMapper Mapper { get; }

        public UpdateCustomerCommandHandler(IMapper mapper, StripeIntegration stripeIntegration, OnlineCheckoutContext context)
        {
            Mapper = mapper;
            StripeIntegration = stripeIntegration;
            Context = context;
        }

        public StripeIntegration StripeIntegration { get; }
        public OnlineCheckoutContext Context { get; }

        public async Task<CustomerDetailModel> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await Context
                .Customers
                .FirstAsync(x => x.Id == request.Id, cancellationToken);

            var stripeResponse = await StripeIntegration.UpdateCustomerAsync(
                Mapper.Map<CustomerUpdateOptions>(request)
            );
            
            customer.StripeCustomerReference = stripeResponse.StripeCustomerReference;
            customer.Email = $"{request.Name}@givtapp.net";
            await Context.SaveChangesAsync(cancellationToken);

            return Mapper.Map<CustomerDetailModel>(customer);
        }
    }
}
