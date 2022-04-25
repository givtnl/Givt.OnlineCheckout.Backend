using AutoMapper;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Update;

public class UpdateOrganisationQueryHandler : IRequestHandler<UpdateOrganisationQuery, OrganisationModel>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public UpdateOrganisationQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<OrganisationModel> Handle(UpdateOrganisationQuery request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map<OrganisationData>(request);
        _context.Add(data).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<OrganisationModel>(data);
    }
}