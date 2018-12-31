namespace LinkCraft.Models.Interfaces
{
    public interface IUserProfile
    {
        string Username { get; set; }
        string Name { get; set; }
        string City { get; set; }
        string Country { get; set; }
        int CreatedArtCodes { get; set; }
    }
}
