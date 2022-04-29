using AutoMapper;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Read;

public class ReadOrganisationMediumQueryHandler : IRequestHandler<ReadOrganisationMediumQuery, MediumDetailModel>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public ReadOrganisationMediumQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<MediumDetailModel> Handle(ReadOrganisationMediumQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Mediums
            .Where(x => x.Medium == request.MediumId && x.OrganisationId == request.OrganisationId)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        return _mapper.Map<MediumDetailModel>(data);
    }
}