using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.ComponentModel.DataAnnotations;
using static Brokerage.Models.Users;

namespace Brokerage.DTOs
{
    public class CreateAdminDTO
    {
        public string? Username { get; set; }
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; }
        
    }
}
