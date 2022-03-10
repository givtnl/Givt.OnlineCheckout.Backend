using AutoMapper;
using Givt.OnlineCheckout.API.Example.Integration;
using Givt.OnlineCheckout.API.Example.Integration.SDK;
using Givt.OnlineCheckout.DataAccess;
using Givt.OnlineCheckout.DataAccess.DataModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.API.Example.Business
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
            var integrationRequestModel = Mapper.Map<UpdateCustomerCommand, CustomerUpdateOptions>(request);
            var integrationResponseModel = Mapper.Map<Customer, CustomerDetailModel>(await StripeIntegration.UpdateCustomerAsync(integrationRequestModel));
            var customer = await Context.Customers.FirstAsync(x => x.Id == integrationRequestModel.Id, cancellationToken);
            customer.StripeCustomerReference = integrationResponseModel.StripeCustomerReference;
            customer.Email = $"slunse@{request.Name}.zugteran";
            await Context.SaveChangesAsync(cancellationToken);
            return new CustomerDetailModel { Id = customer.Id, Email = customer.Email, StripeCustomerReference = integrationResponseModel.StripeCustomerReference };
        }
    }
}
