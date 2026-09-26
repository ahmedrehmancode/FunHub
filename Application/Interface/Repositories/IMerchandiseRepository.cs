using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repositories
{
    public interface IMerchandiseRepository : IGenricRepository<MerchandiseItem>
    {
        Task<IEnumerable<MerchandiseItem>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<MerchandiseItem>> GetUpcomingAsync();
    }
}
