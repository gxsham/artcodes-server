using System;
using System.ComponentModel.DataAnnotations;

namespace LinkCraft.Models
{
    public class RegistrationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
