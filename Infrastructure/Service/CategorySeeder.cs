using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public static class CategorySeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.Categorys.AnyAsync()) return;

            var categories = new List<Category>
            {
                new() { Name = "Anime"},
                new() { Name = "Gaming"},
                new() { Name = "Movies"},
                new() { Name = "TV Shows"},
                new() { Name = "K-Pop"},
                new() { Name = "Comics"},
                new() { Name = "Manga"},
                new() { Name = "Cosplay"},
            };

            await context.Categorys.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }
    }
}
