using AutoMapper;
using Givt.OnlineCheckout.API.Example.Integration;
using Givt.OnlineCheckout.API.Example.Integration.SDK;
using Givt.OnlineCheckout.DataAccess;
using Givt.OnlineCheckout.DataAccess.DataModels;
using MediatR;

namespace Givt.OnlineCheckout.API.Example.Business
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
            var integrationRequestModel = Mapper.Map<CreateCustomerCommand, CustomerCreateOptions>(request);
            var integrationResponseModel = Mapper.Map<Customer, CustomerDetailModel>(await StripeIntegration.CreateCustomerAsync(integrationRequestModel));
            var customer = new DataCustomer { Email = $"random@{request.Name}.noiceeeeuh", StripeCustomerReference = integrationResponseModel.StripeCustomerReference };
            await Context.AddAsync(customer, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
            return new CustomerDetailModel { Id = customer.Id, Email = customer.Email, StripeCustomerReference = integrationResponseModel.StripeCustomerReference };
        }
    }
}