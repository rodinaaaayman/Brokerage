using MediatR;
using static Brokerage.Models.Orders;

namespace Brokerage.Application.Services.orders.Commands.PlaceOrder;

public class PlaceOrderCommand : IRequest<Models.Orders>
{
    public int Id { get; set; }

    public OrderTypes OrderType { get; set; }

    public decimal LimitPrice { get; set; }

    public int UnitPrice { get; set; }

    public int Quantity { get; set; }
}