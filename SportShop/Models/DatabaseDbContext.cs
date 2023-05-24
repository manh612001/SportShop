using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportShop.Models
{
    public class DatabaseDbContext:IdentityDbContext<AppUser>
    {
        public DatabaseDbContext(DbContextOptions<DatabaseDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasKey(x => new { x.CartId, x.CartItemId });
            modelBuilder.Entity<Order>()
                .HasOne(si => si.Cart)
                .WithMany(si => si.Orders)
                .HasForeignKey(si => si.CartId);
            modelBuilder.Entity<Order>()
                .HasOne(si => si.CartItem)
                .WithMany(si => si.Orders)
                .HasForeignKey(si => si.CartItemId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }    
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        
    }
}
