using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Persistence
{
    internal sealed class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options) { }
        public DbSet<Domain.Payment> Payments => Set<Domain.Payment>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("payment");
            modelBuilder.HasSequence<long>("PaymentNoSeq", schema: "payment")
            .StartsAt(1).IncrementsBy(1);
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        }
    }
}