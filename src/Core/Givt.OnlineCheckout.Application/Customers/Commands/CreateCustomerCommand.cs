using AutoMapper;
using Givt.OnlineCheckout.Application.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Stripe;
using Givt.OnlineCheckout.Integrations.Stripe.SDK;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;

namespace Givt.OnlineCheckout.Application.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<CustomerDetailModel>
    {
        public string Name { get; set; }
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDetailModel>
    {
        public IMapper Mapper { get; }
        public StripeIntegration StripeIntegration { get; }
        public OnlineCheckoutContext Context { get; }

        public CreateCustomerCommandHandler(IMapper mapper, StripeIntegration integrationService, OnlineCheckoutContext context)
        {
            Mapper = mapper;
            StripeIntegration = integrationService;
            Context = context;
        }
        public async Task<CustomerDetailModel> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var stripeResponse = await StripeIntegration
                .CreateCustomerAsync(Mapper.Map<CustomerCreateOptions>(request));

            var customer = new DataCustomer
            {
                Email = $"random@{request.Name}.noiceeeeuh",
                StripeCustomerReference =
                stripeResponse.StripeCustomerReference
            };

            await Context.AddAsync(customer, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);

            return Mapper.Map<CustomerDetailModel>(customer);
        }
    }
}