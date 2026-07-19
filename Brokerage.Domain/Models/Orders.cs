
using FluentValidation;
using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static Brokerage.Models.Orders;   

namespace Brokerage.Models
{
    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [ForeignKey("Client")]
        public int Id { get; set; }
        public OrderTypes OrderType { get; set; } = OrderTypes.Market;
        public enum OrderTypes { Market, Limit }
        public decimal LimitPrice { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }
        public int FilledQuantity { get; set; } = 0;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal NetAmount { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Commission { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal GrossAmount { get; private set; }
        public decimal CommissionRate { get; set; } = 0.005m;
        public enum OrderStatus
        {
            Pending,
            PartiallyFilled,
            Filled, 
            Canceled
        }
        [StringLength(50)]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        [JsonIgnore]
        public Clients? Client { get; set; }
        public ICollection<Executions> Executions { get; set; } = new List<Executions>();
        [JsonIgnore]
        public Invoice? Invoice { get; set; }
    }
    //public class CreateOrderDTOValidator : AbstractValidator<CreateOrdersDTO>
    //{
    //    public CreateOrderDTOValidator()
    //    {
    //        RuleFor(x => x.LimitPrice)
    //            .NotNull()
    //            .GreaterThan(0)
    //            .When(x => x.OrderType == OrderTypes.Limit)
    //            .WithMessage("Limit Price is required and must be greater than 0 for Limit orders.");

    //        RuleFor(x => x.LimitPrice)
    //            .Null()
    //            .When(x => x.OrderType != OrderTypes.Limit)
    //            .WithMessage("Limit Price can only be specified for Limit orders.");
    //    }
    //}
}
