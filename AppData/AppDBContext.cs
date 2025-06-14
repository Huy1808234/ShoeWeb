using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project01.Data;
using Project01.Models;

namespace Project01.AppData
{
    public class AppDBContext: IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<OrderProduct> OrderProducts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; }= null!;
        public DbSet<Checkout> Checkouts { get; set; }
    }
}
