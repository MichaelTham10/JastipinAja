using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Persistence
{
    internal sealed class PaymentConfiguration : IEntityTypeConfiguration<Domain.Payment>
    {
        public void Configure(EntityTypeBuilder<Domain.Payment> builder)
        {
            builder.ToTable("payments");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedOnAdd();          // identity/auto-increment

            builder.Property(o => o.PublicId).IsRequired();
            builder.HasIndex(o => o.PublicId).IsUnique();              // unik: referensi teknis.

            builder.Property(o => o.OrderPublicId).IsRequired();

            builder.Property(o => o.PaymentNo).IsRequired();
            builder.HasIndex(o => o.PaymentNo).IsUnique();              // unik: referensi bisnis.

            builder.Property(o => o.Amount).HasColumnType("decimal(18,2)");

            builder.Property(o => o.Status).HasConversion<string>().HasMaxLength(30);

            builder.Property(o => o.CreatedAt).IsRequired();
        }
    }
}
