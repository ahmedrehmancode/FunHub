using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repositories
{
    public interface IBookmarkRepository : IGenricRepository<Bookmark>
    {
        Task<bool> ExistsAsync(string userId, int contentId);
        Task<Bookmark?> GetByUserAndContentAsync(string userId, int contentId);
        Task<IEnumerable<Bookmark>> GetByUserAsync(string userId);
    }
}
