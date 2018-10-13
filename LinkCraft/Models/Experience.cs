using System;
using System.ComponentModel.DataAnnotations;

namespace LinkCraft.Models
{
    public class Experience
    {
        public Guid Id { get; set;}
        [Required]
        public string Code { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        public Experience()
        {
            Id = Guid.NewGuid();
        }
    }
}
