using System.ComponentModel.DataAnnotations;

namespace Brokerage.DTOs
{
    public class CreateClientsDTO
    {
        [Required]
        [StringLength(50)]
        public string? Username { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(14, MinimumLength = 14)]
        public string NationalID { get; set; } = string.Empty;

        [StringLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        public decimal Deposit { get; set; }
        public string Role { get; private set; } = "Client";
    }
}
