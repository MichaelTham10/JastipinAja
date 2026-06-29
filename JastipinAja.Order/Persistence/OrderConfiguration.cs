using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Persistence
{
    internal sealed class OrderConfiguration : IEntityTypeConfiguration<Domain.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedOnAdd();          // identity/auto-increment

            builder.Property(o => o.PublicId).IsRequired();
            builder.HasIndex(o => o.PublicId).IsUnique();              // unik: referensi teknis

            builder.Property(o => o.OrderNo).IsRequired().HasMaxLength(30);
            builder.HasIndex(o => o.OrderNo).IsUnique();

            builder.Property(o => o.CustomerId).IsRequired();

            builder.Property(o => o.ItemDescription).IsRequired().HasMaxLength(500);

            builder.Property(o => o.Price).HasColumnType("decimal(18,2)");

            builder.Property(o => o.Status).HasConversion<string>().HasMaxLength(30);

            builder.Property(o => o.CreatedAt).IsRequired();
        }
    }
}
