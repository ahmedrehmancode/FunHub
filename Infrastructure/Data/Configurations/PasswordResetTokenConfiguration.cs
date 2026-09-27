using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    using Domain.Entity;
    using global::Infrastructure.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace Infrastructure.Data.Configurations
    {
        public class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
        {
            public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
            {
                builder.HasKey(t => t.Id);

                builder.Property(t => t.UserId)
                       .IsRequired();

                builder.Property(t => t.TokenHash)
                       .IsRequired();

                builder.Property(t => t.ExpiresAt)
                       .IsRequired();

                builder.HasOne<ApplicationUser>()
                   .WithMany()
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

                builder.ToTable("PasswordResetTokens");
            }
        }
    }
}
