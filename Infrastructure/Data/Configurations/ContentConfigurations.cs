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
    public class ContentConfigurations : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.Property(c => c.Type)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.HasOne(c => c.Category)
                .WithMany(cat => cat.Contents)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Description)
                   .HasMaxLength(10000);

            builder.HasIndex(c => c.CategoryId);   

            builder.Property(c => c.PopularityScore)
                   .HasDefaultValue(0);
        }
    }
}
