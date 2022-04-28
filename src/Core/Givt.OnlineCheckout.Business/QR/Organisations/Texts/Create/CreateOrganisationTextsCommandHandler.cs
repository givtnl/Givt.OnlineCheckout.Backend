using AutoMapper;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Texts.Create;

public class CreateOrganisationTextsCommandHandler : IRequestHandler<CreateOrganisationTextsCommand, CreateOrganisationTextsResult>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public CreateOrganisationTextsCommandHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<CreateOrganisationTextsResult> Handle(CreateOrganisationTextsCommand request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map<OrganisationTexts>(request);
        _context.Add(data);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CreateOrganisationTextsResult>(data);
    }
}