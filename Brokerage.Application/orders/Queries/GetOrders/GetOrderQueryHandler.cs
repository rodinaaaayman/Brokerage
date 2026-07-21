using Brokerage.Application.Interfaces;
using Brokerage.Application.Orders.Queries.GetOrders;
using Brokerage.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Brokerage.Application.Queries.Orders.GetOrders;

public class GetOrdersQueryHandler
    : IRequestHandler<GetOrdersQuery, IEnumerable<OrdersDTO>>
{
    private readonly IApplicationDbContext _context;

    public GetOrdersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<OrdersDTO>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .Select(o => new OrdersDTO
            {
                OrderId = o.OrderId,
                Quantity = o.Quantity,
                UnitPrice = o.UnitPrice,
                ClientId = o.Id
            })
            .ToListAsync(cancellationToken);

        return orders;
    }
}
