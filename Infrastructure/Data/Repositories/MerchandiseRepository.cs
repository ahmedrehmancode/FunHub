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
    public class MerchandiseRepository : GenricRepository<MerchandiseItem>, IMerchandiseRepository
    {
        private readonly ApplicationDbContext _context;
        public MerchandiseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MerchandiseItem>> GetByCategoryAsync(int categoryId)
        {
            return await _context.MerchandiseItems
            .Where(m => m.CategoryId == categoryId && m.IsActive)
            .ToListAsync();
        }

        public async Task<IEnumerable<MerchandiseItem>> GetUpcomingAsync()
        {
            return await _context.MerchandiseItems
            .Where(m => m.IsUpcoming && m.IsActive)
            .ToListAsync();
        }
    }
}
