using System.ComponentModel.DataAnnotations;

namespace Brokerage.Models
{
    public class Clients : Users
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required]
        [StringLength(14, MinimumLength = 14)]
        public string NationalID { get; set; } = string.Empty;
        [StringLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        public Decimal AccountBalance { get ; set; }
        public void Deposit(decimal amount) { AccountBalance += amount; }
        public void Withdraw(decimal amount) { AccountBalance -= amount; }
        public bool IsActive { get; set; } = true;

        public ICollection<Orders> Orders { get; set; } = new List<Orders>();
        public ICollection<Brokers> Brokers { get; set; }
        = new List<Brokers>();
    }
}
