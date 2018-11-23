using System.ComponentModel.DataAnnotations;
using LinkCraft.Models.Interfaces;

namespace LinkCraft.Models
{
    public class PostExperienceViewModel : IBaseExperience
    {
        [Required]
        public string Code { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
    }
}
