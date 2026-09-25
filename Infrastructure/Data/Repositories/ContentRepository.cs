using Application.Common.Models;
using Application.Interface.Repositories;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ContentRepository : GenricRepository<Content>, IContentRepository
    {
        private readonly ApplicationDbContext _context;
        public ContentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByTitleInCategoryAsync(string title, int categoryId)
        {
            return await _context.Contents.AnyAsync(c =>
            c.Title.ToLower() == title.ToLower() && c.CategoryId == categoryId);
        }

        public async Task<Content?> GetByIdWithCategoryAsync(int id)
        {
            return await _context.Contents
            .Include(c => c.Category)
            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<PagedResult<Content>> GetFilteredAsync(ContentFilterParams filters)
        {
            var query = _context.Contents
            .Include(c => c.Category)
            .Where(c => c.IsActive)
            .AsQueryable();

            if (filters.CategoryId.HasValue)
                query = query.Where(c => c.CategoryId == filters.CategoryId.Value);

            if (!string.IsNullOrWhiteSpace(filters.Genre))
                query = query.Where(c => c.Genre.ToLower() == filters.Genre.ToLower());

            if (filters.Type.HasValue)
                query = query.Where(c => c.Type == filters.Type.Value);

            if (filters.ReleaseYear.HasValue)
                query = query.Where(c => c.ReleaseDate.HasValue
                    && c.ReleaseDate.Value.Year == filters.ReleaseYear.Value);

            if (!string.IsNullOrWhiteSpace(filters.SearchTerm))
                query = query.Where(c => c.Title.Contains(filters.SearchTerm)
                    || c.Description.Contains(filters.SearchTerm));

            query = filters.SortBy?.ToLower() switch
            {
                "popular" => query.OrderByDescending(c => c.PopularityScore),
                "alphabetical" => query.OrderBy(c => c.Title),
                _ => query.OrderByDescending(c => c.CreatedAt) // "latest" default
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((filters.PageNumber - 1) * filters.PageSize)
                .Take(filters.PageSize)
                .ToListAsync();

            return new PagedResult<Content>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = filters.PageNumber,
                PageSize = filters.PageSize
            };
        }
    }
}
