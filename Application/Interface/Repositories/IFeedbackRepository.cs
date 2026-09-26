using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repositories
{
    public interface IFeedbackRepository : IGenricRepository<Feedback>
    {
        Task<IEnumerable<Feedback>> GetByUserAsync(string userId);
    }
}
