using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(x => x.Gender)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(x => x.AvatarUrl)
                .IsRequired(false)
                .HasMaxLength(150);
        }
    }
}
