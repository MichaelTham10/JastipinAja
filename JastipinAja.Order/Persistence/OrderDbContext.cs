using Microsoft.EntityFrameworkCore;


namespace JastipinAja.Order.Persistence
{
    internal sealed class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        public DbSet<Domain.Order> Orders => Set<Domain.Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ordering");
            modelBuilder.HasSequence<long>("OrderNoSeq", schema: "ordering")
            .StartsAt(1).IncrementsBy(1);
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
        }
    }
}
