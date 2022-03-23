using AutoMapper;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;

namespace Givt.OnlineCheckout.Business.Donors.Commands
{
    public class CreateDonorCommand : IRequest<DonorDetailModel>
    {
        public string Name { get; set; }
    }

    public class CreateDonorCommandHandler : IRequestHandler<CreateDonorCommand, DonorDetailModel>
    {
        public IMapper Mapper { get; }
        public OnlineCheckoutContext Context { get; }

        public CreateDonorCommandHandler(IMapper mapper, OnlineCheckoutContext context)
        {
            Mapper = mapper;
            Context = context;
        }
        public async Task<DonorDetailModel> Handle(CreateDonorCommand request, CancellationToken cancellationToken)
        {
            var donor = new DonorData
            {
                Email = $"random@{request.Name}.noiceeeeuh" // TODO: check/limit to 254 char (RFC 2821)
            };

            await Context.AddAsync(donor, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);

            return Mapper.Map<DonorDetailModel>(donor);
        }
    }
}