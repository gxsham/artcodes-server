using LinkCraft.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinkCraft.Data
{
    public class LinkCraftContext : IdentityDbContext<User>
    {
        public LinkCraftContext (DbContextOptions<LinkCraftContext> options)
            : base(options)
        {
        }
        
        public DbSet<Experience> Experience { get; set; }
    }
}
