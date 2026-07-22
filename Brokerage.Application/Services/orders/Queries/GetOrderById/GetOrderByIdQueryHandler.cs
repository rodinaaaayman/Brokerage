using Brokerage.Application.Interfaces;
using Brokerage.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetOrderByIdQueryHandler
    : IRequestHandler<GetOrderByIdQuery, Orders>
{
    private readonly IApplicationDbContext _context;

    public GetOrderByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Brokerage.Models.Orders> Handle(
        GetOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(
                o => o.OrderId == request.OrderId,
                cancellationToken);

        if (order == null)
        {
            throw new Exception("Order not found.");
        }

        return order;
    }
}