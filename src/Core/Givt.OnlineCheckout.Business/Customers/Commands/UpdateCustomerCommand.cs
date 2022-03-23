using AutoMapper;
using Givt.OnlineCheckout.API.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Stripe;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.API.Customers.Commands
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
            var customer = await Context.Customers.FirstAsync(x => x.Id == request.Id, cancellationToken);

            customer.Email = $"{request.Name}@givtapp.net"; // TODO: check/limit to 254 char (RFC 2821)

            await Context.SaveChangesAsync(cancellationToken);

            return Mapper.Map<CustomerDetailModel>(customer);
        }
    }
}
