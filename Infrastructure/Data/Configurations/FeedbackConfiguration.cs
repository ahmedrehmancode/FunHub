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
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.Property(f => f.UserId)
               .IsRequired()
               .HasMaxLength(450);   

            builder.Property(f => f.Type)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(f => f.Message)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(f => f.Star)
                   .IsRequired();

            builder.HasIndex(f => f.UserId);
        }
    }
}
