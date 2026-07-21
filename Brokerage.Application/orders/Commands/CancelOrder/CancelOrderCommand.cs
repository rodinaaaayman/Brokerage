 using MediatR;

    namespace Brokerage.Application.Orders.Commands.CancelOrder;

    public class CancelOrderCommand : IRequest<bool>
    {
        public int OrderId { get; set; }

        public CancelOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }

