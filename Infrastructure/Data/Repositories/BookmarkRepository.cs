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
    public class BookmarkRepository : GenricRepository<Bookmark>, IBookmarkRepository
    {
        private readonly ApplicationDbContext _context;
        public BookmarkRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string userId, int contentId)
        {
            return await _context.Bookmarks
           .AnyAsync(b => b.UserId == userId && b.ContentId == contentId);
        }

        public async Task<Bookmark?> GetByUserAndContentAsync(string userId, int contentId)
        {
            return await _context.Bookmarks
             .FirstOrDefaultAsync(b => b.UserId == userId && b.ContentId == contentId);
        }

        public async Task<IEnumerable<Bookmark>> GetByUserAsync(string userId)
        {
            return await _context.Bookmarks
            .Include(b => b.Content)
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();
        }
    }
}
