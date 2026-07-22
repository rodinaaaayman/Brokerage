using System;
using System.Collections.Generic;
using System.Text;
using Brokerage.Application.Interfaces;
    using global::Brokerage.Application.Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

namespace Brokerage.Application.Services.clients.Commands.UpdateClient;



public class UpdateClientCommandHandler
    : IRequestHandler<UpdateClientCommand, bool>
{

    private readonly IApplicationDbContext _context;


    public UpdateClientCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<bool> Handle(
        UpdateClientCommand request,
        CancellationToken cancellationToken)
    {

        var client = await _context.Clients
            .FirstOrDefaultAsync(
                c => c.Id == request.Id && c.IsActive,
                cancellationToken);


        if (client == null)
        {
            return false;
        }


        client.Name = request.Client.Name;
        client.PhoneNumber = request.Client.PhoneNumber;


        await _context.SaveChangesAsync(cancellationToken);


        return true;
    }
}
