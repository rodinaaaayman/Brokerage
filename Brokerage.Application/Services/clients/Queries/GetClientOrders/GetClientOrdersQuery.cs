using Brokerage.Application.DTOs;
using MediatR;

namespace Brokerage.Application.Services.clients.Queries.GetClientOrders
{
    public record GetClientOrdersQuery(int Id)
            : IRequest<List<OrderSummaryDto>>;
    
}
