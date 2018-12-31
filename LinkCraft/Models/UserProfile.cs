using LinkCraft.Models.Interfaces;

namespace LinkCraft.Models
{
    public class UserProfile : IUserProfile
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int CreatedArtCodes { get; set; }
    }
}
