namespace PhungDKH.Ordering.Domain.Entities.Contexts
{
    using Microsoft.EntityFrameworkCore;

    public class AppOrderingDbContext : DbContext
    {
        public AppOrderingDbContext(DbContextOptions<AppOrderingDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasIndex(b => b.UserId);

            modelBuilder.Entity<OrderDetail>()
                .HasIndex(b => b.OrderId);
        }
    }
}
