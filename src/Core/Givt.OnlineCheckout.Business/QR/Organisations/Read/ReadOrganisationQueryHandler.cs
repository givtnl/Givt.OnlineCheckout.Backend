using AutoMapper;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Read;

public class ReadOrganisationQueryHandler : IRequestHandler<ReadOrganisationQuery, OrganisationModel>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public ReadOrganisationQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<OrganisationModel> Handle(ReadOrganisationQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Organisations
            .Where(x => x.Id == request.OrganisationId)            
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        return _mapper.Map<OrganisationModel>(data);
    }
}