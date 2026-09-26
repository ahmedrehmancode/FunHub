using Domain.Entity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext
            (DbContextOptions<ApplicationDbContext> opt) : base(opt) { }

        // Tables
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<MerchandiseItem> MerchandiseItems { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }


        // All Configuration Regsiter
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
