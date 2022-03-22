using AutoMapper;
using Givt.OnlineCheckout.API.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;

namespace Givt.OnlineCheckout.API.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<CustomerDetailModel>
    {
        public string Name { get; set; }
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDetailModel>
    {
        public IMapper Mapper { get; }
        public OnlineCheckoutContext Context { get; }

        public CreateCustomerCommandHandler(IMapper mapper, OnlineCheckoutContext context)
        {
            Mapper = mapper;
            Context = context;
        }
        public async Task<CustomerDetailModel> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new CustomerData
            {
                Email = $"random@{request.Name}.noiceeeeuh"
            };

            await Context.AddAsync(customer, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);

            return Mapper.Map<CustomerDetailModel>(customer);
        }
    }
}