using AutoMapper;
using Givt.OnlineCheckout.Business.Exceptions;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Delete;

public class DeleteOrganisationMediumTextsCommandHandler : IRequestHandler<DeleteOrganisationMediumTextsCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public DeleteOrganisationMediumTextsCommandHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<bool> Handle(DeleteOrganisationMediumTextsCommand request, CancellationToken cancellationToken)
    {
        var data = await _context.Set<MediumTexts>()
            .Where(t => t.LanguageId == request.LanguageId && t.Medium.Medium == request.MediumId)
            .FirstOrDefaultAsync(cancellationToken);
        if (data == null)
            throw new NotFoundException(nameof(MediumTexts), request);

        data.ConcurrencyToken = request.ConcurrencyToken;
        _context.Remove(data);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}