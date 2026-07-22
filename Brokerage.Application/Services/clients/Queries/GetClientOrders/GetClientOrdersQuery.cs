using System;
using System.Collections.Generic;
using System.Text;
using Brokerage.DTOs;
using Brokerage.Application.DTOs;
using MediatR;

namespace Brokerage.Application.Services.clients.Queries.GetClientOrders
{
    public record GetClientOrdersQuery(int Id)
            : IRequest<List<OrderSummaryDto>>;
    
}
