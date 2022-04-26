using AutoMapper;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;

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
        // TODO: fetch medium from DB
        var data = _mapper.Map<MediumTexts>(request);
        _context.Add(data);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CreateOrganisationMediumTextsResult>(data);
    }
}