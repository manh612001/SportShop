using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportShop.Models
{
    public class DatabaseDbContext:IdentityDbContext<AppUser>
    {
        public DatabaseDbContext(DbContextOptions<DatabaseDbContext> options) : base(options) { }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Products { get; set; }
        
    }
}
