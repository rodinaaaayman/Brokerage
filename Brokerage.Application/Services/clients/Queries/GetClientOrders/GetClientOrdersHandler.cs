using Brokerage.Application.Interfaces;
using Brokerage.Application.Services.clients.Queries.GetClientOrders;
using Brokerage.DTOs;
using global::Brokerage.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace Brokerage.Application.Services.clients.Queries
{
    

    namespace Brokerage.Application.Orders.Queries
    {
        public class GetClientOrdersHandler
            : IRequestHandler<GetClientOrdersQuery, List<OrderSummaryDto>>
        {

            private readonly IApplicationDbContext _context;


            public GetClientOrdersHandler(IApplicationDbContext context)
            {
                _context = context;
            }


            public async Task<List<OrderSummaryDto>> Handle(
                GetClientOrdersQuery request,
                CancellationToken cancellationToken)
            {

                var orders = await _context.Orders

                    // filter by client
                    .Where(o => o.Id == request.Id)

                    // projection directly to DTO
                    .Select(o => new OrderSummaryDto
                    {
                        OrderId = o.OrderId,
                        OrderType = o.OrderType.ToString(),
                        Quantity = o.Quantity,
                        UnitPrice = o.UnitPrice,
                        Status = o.Status.ToString(),
                    })

                    .ToListAsync(cancellationToken);


                return orders;
            }
        }
    }
}
