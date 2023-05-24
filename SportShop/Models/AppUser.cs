using Microsoft.AspNetCore.Identity;

namespace SportShop.Models
{
    public class AppUser:IdentityUser
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public DateTime? Dob { get; set; }
        public string? Role { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
