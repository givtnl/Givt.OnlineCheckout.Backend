using AutoMapper;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Read;

public class ReadOrganisationQueryHandler : IRequestHandler<ReadOrganisationQuery, OrganisationDetailModel>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public ReadOrganisationQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<OrganisationDetailModel> Handle(ReadOrganisationQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Organisations
            .Where(o => o.Id == request.OrganisationId)            
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        return _mapper.Map<OrganisationDetailModel>(data);
    }
}