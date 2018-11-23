using System;
using System.ComponentModel.DataAnnotations;
using LinkCraft.Models.Interfaces;

namespace LinkCraft.Models
{
    public class Experience : IExperience
    {
        public Guid Id { get; set;}
        [Required]
        public string Code { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
        public string UserId { get; set; }
        public Experience()
        {
            Id = Guid.NewGuid();
        }
    }
}
