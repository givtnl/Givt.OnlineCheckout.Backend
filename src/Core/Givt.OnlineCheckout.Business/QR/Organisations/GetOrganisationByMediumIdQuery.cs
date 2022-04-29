using AutoMapper;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations;

public class GetOrganisationByMediumIdQuery : IRequest<OrganisationFlattenedModel>
{
    public MediumIdType MediumId { get; set; }
}

public class GetOrganisationByMediumIdQueryHandler : IRequestHandler<GetOrganisationByMediumIdQuery, OrganisationFlattenedModel>
{
    public IMapper Mapper { get; }
    public OnlineCheckoutContext Context { get; }
    public GetOrganisationByMediumIdQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        Mapper = mapper;
        Context = context;
    }

    public async Task<OrganisationFlattenedModel> Handle(GetOrganisationByMediumIdQuery request, CancellationToken cancellationToken)
    {
        return Mapper.Map<OrganisationFlattenedModel>(
            await Context.Organisations.FirstOrDefaultAsync(x => x.Namespace == request.MediumId, cancellationToken)
        );
    }
}
