using Application.Common.Models;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repositories
{
    public interface IContentRepository : IGenricRepository<Content>
    {
        Task<Content?> GetByIdWithCategoryAsync(int id);
        Task<PagedResult<Content>> GetFilteredAsync(ContentFilterParams filters);
        Task<bool> ExistsByTitleInCategoryAsync(string title, int categoryId);
        Task<IEnumerable<Content>> GetAllActiveContentAsync();
    }
}
