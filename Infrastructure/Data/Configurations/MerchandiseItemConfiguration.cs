using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class MerchandiseItemConfiguration : IEntityTypeConfiguration<MerchandiseItem>
    {
        public void Configure(EntityTypeBuilder<MerchandiseItem> builder)
        {
            builder.Property(m => m.Name)
               .IsRequired()
               .HasMaxLength(150);

            builder.Property(m => m.Tag)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(m => m.ImageUrl)
                   .HasMaxLength(500)
                   .IsRequired(false);

            builder.Property(m => m.IsUpcoming)
                   .HasDefaultValue(false);

            builder.Property(m => m.ViewCount)
                   .HasDefaultValue(0);

            builder.Property(m => m.IsActive)
                   .HasDefaultValue(true);

            builder.HasOne(m => m.Category)
                   .WithMany(c => c.MerchandiseItems)
                   .HasForeignKey(m => m.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(m => m.CategoryId);
            builder.HasIndex(m => m.IsUpcoming);
        }
    }
}
