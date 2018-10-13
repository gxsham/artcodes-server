using System;
using Microsoft.AspNetCore.Identity;

namespace LinkCraft.Models
{
    public class User: IdentityUser
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Birthday { get; set; }
    }
}
