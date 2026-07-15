namespace Brokerage.DTOs
{
    public class CreateOrdersDTO
    {
        public int ClientId { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
