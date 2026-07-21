using Brokerage.DTOs;
using MediatR;

namespace Brokerage.Application.Orders.Queries.GetOrders;

public class GetOrdersQuery : IRequest<IEnumerable<OrdersDTO>>
{
}
