using System;
using System.Collections.Generic;
using System.Text;
using Brokerage.Application.DTOs;
using Brokerage.Application.Interfaces;
using global::Brokerage.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Brokerage.Application.Services.clients.Queries.GetClients
{

    public class GetClientsQueryHandler
        : IRequestHandler<GetClientsQuery, IEnumerable<ClientsDTO>>
    {
        private readonly IApplicationDbContext _context;

        public GetClientsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<ClientsDTO>> Handle(
            GetClientsQuery request,
            CancellationToken cancellationToken)
        {
            var clients = await _context.Clients
                .Where(c => c.IsActive)
                .Select(c => new ClientsDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    NationalID = c.NationalID,
                    PhoneNumber = c.PhoneNumber,
                    AccountBalance = c.AccountBalance
                })
                .ToListAsync(cancellationToken);


            return clients;
        }
    }
}
