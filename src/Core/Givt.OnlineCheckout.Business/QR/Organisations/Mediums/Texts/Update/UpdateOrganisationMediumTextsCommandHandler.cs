using AutoMapper;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Update;

public class UpdateOrganisationMediumTextsCommandHandler : IRequestHandler<UpdateOrganisationMediumTextsCommand, UpdateOrganisationMediumTextsResult>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public UpdateOrganisationMediumTextsCommandHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<UpdateOrganisationMediumTextsResult> Handle(UpdateOrganisationMediumTextsCommand request, CancellationToken cancellationToken)
    {
        // TODO: fetch medium from DB
        var data = _mapper.Map<MediumTexts>(request);
        _context.Add(data).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UpdateOrganisationMediumTextsResult>(data);
    }
}