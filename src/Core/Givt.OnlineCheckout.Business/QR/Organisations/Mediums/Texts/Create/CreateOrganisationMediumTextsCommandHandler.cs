using AutoMapper;
using Givt.OnlineCheckout.Business.Exceptions;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Create;

public class CreateOrganisationMediumTextsCommandHandler : IRequestHandler<CreateOrganisationMediumTextsCommand, CreateOrganisationMediumTextsResult>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public CreateOrganisationMediumTextsCommandHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<CreateOrganisationMediumTextsResult> Handle(CreateOrganisationMediumTextsCommand request, CancellationToken cancellationToken)
    {
        // TODO: validation
        var medium = await _context.Mediums
            .Where(m => m.Medium == request.MediumId && m.OrganisationId == request.OrganisationId)
            .FirstOrDefaultAsync(cancellationToken);
        if (medium == null)
            throw new NotFoundException(nameof(MediumData), request); // TODO: name

        var data = _mapper.Map<MediumTexts>(request);
        data.Medium = medium;
        _context.Add(data);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CreateOrganisationMediumTextsResult>(data);
    }
}