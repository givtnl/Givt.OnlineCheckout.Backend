using AutoMapper;
using Givt.OnlineCheckout.API.Exceptions;
using Givt.OnlineCheckout.API.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.API.Mediums.Queries;

public class GetMediumDetailsQeuryHandler : IRequestHandler<GetMediumDetailsQuery, MediumDetailModel>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public GetMediumDetailsQeuryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<MediumDetailModel> Handle(GetMediumDetailsQuery request, CancellationToken cancellationToken)
    {
        var medium = await _context.Mediums.Where(x => x.Medium == request.MediumId).Include(x => x.Merchant).FirstOrDefaultAsync(cancellationToken);
        
        if (medium == null)
        {
            throw new NotFoundException("Merchant not found for this medium");
        }

        
        return _mapper.Map<DataMedium, MediumDetailModel>(medium);
    }
}