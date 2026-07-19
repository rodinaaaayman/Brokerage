using static Brokerage.Models.Orders;

namespace Brokerage.DTOs;

    public class CreateOrdersDTO
    {
        public int Id { get; set; }
        public OrderTypes OrderType { get; set; }
        public decimal LimitPrice { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
    }

