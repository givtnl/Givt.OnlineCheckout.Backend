using AutoMapper;
using Givt.OnlineCheckout.Business.Exceptions;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Texts.Delete;

public class DeleteOrganisationTextsCommandHandler : IRequestHandler<DeleteOrganisationTextsCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public DeleteOrganisationTextsCommandHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<bool> Handle(DeleteOrganisationTextsCommand request, CancellationToken cancellationToken)
    {
        var data = await _context.Set<OrganisationTexts>()
            .Where(t => t.LanguageId == request.LanguageId && t.OrganisationId == request.OrganisationId)
            .FirstOrDefaultAsync(cancellationToken);
        if (data == null)
            throw new NotFoundException(nameof(OrganisationTexts), request);

        data.ConcurrencyToken = request.ConcurrencyToken;
        _context.Remove(data);
        await _context.SaveChangesAsync(cancellationToken);
        return true;

    }
}