using Brokerage.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Brokerage.Application.Orders.Commands.CancelOrder;

public class CancelOrderCommandHandler
    : IRequestHandler<CancelOrderCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public CancelOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<bool> Handle(
        CancelOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(
                o => o.OrderId == request.OrderId,
                cancellationToken);


        if (order == null)
        {
            return false;
        }


        _context.Orders.Remove(order);

        await _context.SaveChangesAsync(cancellationToken);


        return true;
    }
}
