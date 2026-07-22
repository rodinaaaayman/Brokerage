using Brokerage.Application.Interfaces;
using Brokerage.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Brokerage.Application.Services.clients.Queries.GetClientById;


public class GetClientByIdQueryHandler
    : IRequestHandler<GetClientByIdQuery, ClientsDTO?>
{

    private readonly IApplicationDbContext _context;


    public GetClientByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<ClientsDTO?> Handle(
        GetClientByIdQuery request,
        CancellationToken cancellationToken)
    {

        var client = await _context.Clients
            .Where(c => c.Id == request.Id && c.IsActive)
            .Select(c => new ClientsDTO
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                NationalID = c.NationalID,
                PhoneNumber = c.PhoneNumber,
                AccountBalance = c.AccountBalance
            })
            .FirstOrDefaultAsync(cancellationToken);


        return client;
    }
}