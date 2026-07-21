using Brokerage.Application.Interfaces;
using Brokerage.Application.orders.Commands.PlaceOrder;
using Brokerage.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Brokerage.Application.orders.Commands.PlaceOrder
{


    public class PlaceOrderCommandHandler
        : IRequestHandler<PlaceOrderCommand, Brokerage.Models.Orders>
    {
        
        private readonly IApplicationDbContext _context;

        public PlaceOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Brokerage.Models.Orders> Handle(
            PlaceOrderCommand request,
            CancellationToken cancellationToken)
        {
            // Logic goes here
            var client = await _context.Clients
        .FirstOrDefaultAsync(c => c.Id == request.Id && c.IsActive);
            if (client == null)
            {
                throw new Exception("Invalid client.");
            }

            var order = new Brokerage.Models.Orders
            {
                Id = request.Id,
                OrderType = request.OrderType,
                LimitPrice = request.LimitPrice,
                UnitPrice = request.UnitPrice,
                Quantity = request.Quantity
            };
            if (client.AccountBalance < order.GrossAmount)
            {
                throw new Exception("Insufficient balance.");
            }

            client.AccountBalance -= order.GrossAmount;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(CancellationToken.None);
            return order;
        }
    }
}
