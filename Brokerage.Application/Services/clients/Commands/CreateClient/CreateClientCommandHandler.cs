using System;
using System.Collections.Generic;
using System.Text;
using Brokerage.Application.Interfaces;
    using Brokerage.Models;
    using MediatR;

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

            var dto = request.Client;


            var client = new Clients
            {
                Username = dto.Username,
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                NationalID = dto.NationalID,
                PhoneNumber = dto.PhoneNumber,
                Role = "Client"
            };


            client.Deposit(dto.Deposit);


            _context.Clients.Add(client);


            await _context.SaveChangesAsync(cancellationToken);


            return client.Id;
        }
    }
}
