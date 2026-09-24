using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Domain.Exceptions
{
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string msg = "You are not authorized.") :base(msg, 401)
        {
            
        }
    }
}
