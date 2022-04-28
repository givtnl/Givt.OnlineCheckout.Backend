using AutoMapper;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Givt.OnlineCheckout.Business.QR.Organisations.List;

public class ListOrganisationsQueryHandler : IRequestHandler<ListOrganisationsQuery, List<OrganisationDetailModel>>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public ListOrganisationsQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<OrganisationDetailModel>> Handle(ListOrganisationsQuery request, CancellationToken cancellationToken)
    {
        // setup
        IQueryable<OrganisationData> query = _context.Organisations;
        // filter
        if (!String.IsNullOrEmpty(request.Filter))
            query = query.Where(x => EF.Functions.ILike(x.Name, $"%{request.Filter}%")); // ILike = case insensitive
        // default sort
        query = query.OrderBy(x => x.Name);

        // pagination
        if (request.Start.HasValue)
            query = query.Skip(((int)request.Start.Value) - 1);
        query = query.Take((int)request.PageSize);

        var data = await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<OrganisationDetailModel>>(data);
    }
}