using System.ComponentModel.DataAnnotations;

namespace Brokerage.DTOs
{
    public class CreateAdminDTO
    {
        public string? Username { get; set; }
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        
    }
}
