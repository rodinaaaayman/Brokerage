using Brokerage.Application.Interfaces;
using Brokerage.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Brokerage.Application.Services.orders.Commands.PlaceOrder
{


    public class PlaceOrderCommandHandler
        : IRequestHandler<PlaceOrderCommand, Models.Orders>
    {
        
        private readonly IApplicationDbContext _context;

        public PlaceOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Models.Orders> Handle(
            PlaceOrderCommand request,
            CancellationToken cancellationToken)
        {
            // Logic goes here
            var client = await _context.Clients
        .FirstOrDefaultAsync(c => c.Id == request.Id && c.IsActive);
            if (client == null)
            {
                throw new KeyNotFoundException("Invalid client.");
            }
        
            var order = new Models.Orders
            {
                Id = request.Id,
                OrderType = request.OrderType,
                LimitPrice = request.LimitPrice,
                UnitPrice = request.UnitPrice,
                Quantity = request.Quantity
                
            };
            client.Withdraw(order.GrossAmount);
            client.AccountBalance -= order.GrossAmount;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(CancellationToken.None);
            return order;
        }
    }
}
