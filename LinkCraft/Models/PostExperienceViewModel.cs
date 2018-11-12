using System.ComponentModel.DataAnnotations;

namespace LinkCraft.Models
{
    public class PostExperienceViewModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
    }
}
