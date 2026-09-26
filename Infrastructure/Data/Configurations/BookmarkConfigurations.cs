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
    public class BookmarkConfigurations : IEntityTypeConfiguration<Bookmark>
    {
        public void Configure(EntityTypeBuilder<Bookmark> builder)
        {
            builder.HasIndex(b => new { b.UserId, b.ContentId })
                .IsUnique();

            builder.HasOne(b => b.Content)
                   .WithMany(c => c.Bookmarks)
                   .HasForeignKey(b => b.ContentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
