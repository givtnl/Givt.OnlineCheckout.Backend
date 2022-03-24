using AutoMapper;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Stripe;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.Donors.Commands
{

    public class UpdateDonorCommand : IRequest<DonorDetailModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdateDonorCommandHandler : IRequestHandler<UpdateDonorCommand, DonorDetailModel>
    {
        public IMapper Mapper { get; }

        public UpdateDonorCommandHandler(IMapper mapper, StripeIntegration stripeIntegration, OnlineCheckoutContext context)
        {
            Mapper = mapper;
            StripeIntegration = stripeIntegration;
            Context = context;
        }

        public StripeIntegration StripeIntegration { get; }
        public OnlineCheckoutContext Context { get; }

        public async Task<DonorDetailModel> Handle(UpdateDonorCommand request, CancellationToken cancellationToken)
        {
            var donor = await Context.Donors.FirstAsync(x => x.Id == request.Id, cancellationToken);

            donor.Email = $"{request.Name}@givtapp.net"; // TODO: check/limit to 70 char ?

            await Context.SaveChangesAsync(cancellationToken);

            return Mapper.Map<DonorDetailModel>(donor);
        }
    }
}
