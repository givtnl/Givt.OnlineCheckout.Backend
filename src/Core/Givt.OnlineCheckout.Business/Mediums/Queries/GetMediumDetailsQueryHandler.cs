using AutoMapper;
using Givt.OnlineCheckout.Business.Exceptions;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.Mediums.Queries;

public class GetMediumDetailsQueryHandler : IRequestHandler<GetMediumDetailsQuery, MediumDetailModel>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public GetMediumDetailsQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<MediumDetailModel> Handle(GetMediumDetailsQuery request, CancellationToken cancellationToken)
    {
        var medium = await _context.Mediums.Where(x => x.Medium == request.MediumId.ToString())
                                            .Include(x => x.Organisation)
                                            .SingleOrDefaultAsync(cancellationToken);
        return medium == null 
            ? throw new NotFoundException(nameof(MediumIdType), request.MediumId)
            : _mapper.Map<MediumData, MediumDetailModel>(medium);
    }
}