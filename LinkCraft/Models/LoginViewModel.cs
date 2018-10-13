using System.ComponentModel.DataAnnotations;

namespace LinkCraft.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [EmailAddress]
        public string Password { get; set; }
    }
}
