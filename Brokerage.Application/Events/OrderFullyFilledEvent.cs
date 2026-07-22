using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Application.Events
{
    public class OrderFullyFilledEvent : INotification
    {
        public int OrderId { get; }

        public OrderFullyFilledEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}
