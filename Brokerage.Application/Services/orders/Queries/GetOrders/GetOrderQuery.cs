using Brokerage.DTOs;
using MediatR;

namespace Brokerage.Application.Services.orders.Queries.GetOrders;


    public record GetOrdersQuery(
    int? Cursor,
    int Limit = 20)
    : IRequest<List<OrdersDTO>>;

