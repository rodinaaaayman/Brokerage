using Brokerage.DTOs;
using MediatR;

namespace Brokerage.Application.Services.orders.Queries.GetOrders;

public class GetOrdersQuery : IRequest<IEnumerable<OrdersDTO>>
{
}
