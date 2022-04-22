using AutoMapper;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.Organisations;

public class ListOrganisationsQueryHandler : IRequestHandler<ListOrganisationsQuery, List<OrganisationModel>>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public ListOrganisationsQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<OrganisationModel>> Handle(ListOrganisationsQuery request, CancellationToken cancellationToken)
    {
        // setup
        IQueryable<OrganisationData> query = _context.Organisations
            .Include(o => o.Country);
        // filter
        if (!String.IsNullOrEmpty(request.Filter))
            query = query.Where(x => x.Name.Contains(request.Filter));
        // default sort
        query = query.OrderBy(x => x.Name);

        // pagination
        if (request.Start.HasValue)
            query = query.Skip(((int)request.Start.Value) - 1);
        query = query.Take((int)request.PageSize);

        var data = await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<OrganisationModel>>(data);
    }
}