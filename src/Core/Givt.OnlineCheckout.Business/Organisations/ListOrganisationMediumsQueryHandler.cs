using AutoMapper;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.Organisations;

public class ListOrganisationMediumsQueryHandler : IRequestHandler<ListOrganisationMediumsQuery, List<MediumDetailModel>>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public ListOrganisationMediumsQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<MediumDetailModel>> Handle(ListOrganisationMediumsQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Organisations
            .Where(x => x.Id == request.OrganisationId)
            .SelectMany(x => x.Mediums)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<MediumDetailModel>>(data);
    }
}