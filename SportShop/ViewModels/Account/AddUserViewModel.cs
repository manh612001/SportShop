
using System.ComponentModel.DataAnnotations;

namespace SportShop.ViewModels.Account
{
    public class AddUserViewModel
    {
        public string Id { get; set; }
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Minimum length 6 and must contain  1 Uppercase,1 lowercase, 1 special character and 1 digit")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public DateTime? Dob { get; set; }
        [Required]
        public string? Role { get; set; }
    }
}
