using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Servies
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateTokenAsync(string userId, string fullname, string gender, IList<string> roles);
    }
}
