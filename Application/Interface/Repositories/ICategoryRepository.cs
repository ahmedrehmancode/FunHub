using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repositories
{
    public interface ICategoryRepository : IGenricRepository<Category>
    {
        Task<bool> ExistsByNameAsync(string name);
        Task<IEnumerable<Category>> GetActiveCategoriesAsync();
    }
}
