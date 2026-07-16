using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [StringLength(50)]
        public string? Status { get; set; } = "Pending";
        [JsonIgnore]
        public Clients? Client { get; set; }
    }
}
