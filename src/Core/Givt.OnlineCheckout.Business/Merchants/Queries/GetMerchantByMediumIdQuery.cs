using AutoMapper;
using Givt.OnlineCheckout.API.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.API.Merchants.Queries
{
    public class GetMerchantByMediumIdQuery : IRequest<MerchantDetailModel>
    {
        public string MediumId { get; set; }
    }

    public class GetMerchantByMediumIdQueryHandler : IRequestHandler<GetMerchantByMediumIdQuery, MerchantDetailModel>
    {
        public IMapper Mapper { get; }
        public OnlineCheckoutContext Context { get; }
        public GetMerchantByMediumIdQueryHandler(IMapper mapper, OnlineCheckoutContext context)
        {
            Mapper = mapper;
            Context = context;
        }

        public async Task<MerchantDetailModel> Handle(GetMerchantByMediumIdQuery request, CancellationToken cancellationToken)
        {
            return Mapper.Map<MerchantDetailModel>(
                await Context.Merchants.FirstOrDefaultAsync(x => x.Namespace == request.MediumId, cancellationToken)
            );
        }
    }
}
