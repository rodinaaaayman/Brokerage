using Brokerage.Application.Interfaces;
using Brokerage.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Brokerage.Application.Services.clients.Commands.CreateClient
{


    public class CreateClientCommandHandler
        : IRequestHandler<CreateClientCommand, int>
    {
        private readonly IApplicationDbContext _context;


        public CreateClientCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<int> Handle(
            CreateClientCommand request,
            CancellationToken cancellationToken)
        {
            if (await _context.Clients.AnyAsync(
        c => c.Email == request.Email,
        cancellationToken))
            {
                throw new InvalidOperationException("Email already exists.");
            }

            if (await _context.Clients.AnyAsync(
        c => c.NationalID == request.NationalID,
        cancellationToken))
            {
                throw new InvalidOperationException("National ID already exists.");
            }

            var client = new Clients
            {
                Username = request.Username,
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                NationalID = request.NationalID,
                PhoneNumber = request.PhoneNumber,
                Role = "Client"
            };

            client.Deposit(request.Deposit);

            _context.Clients.Add(client);

            await _context.SaveChangesAsync(cancellationToken);

            return client.Id;
        }
    }
}
