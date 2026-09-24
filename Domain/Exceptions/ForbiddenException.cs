using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Domain.Exceptions
{
    public class ForbiddenException : AppException
    {
        public ForbiddenException(string msg = "You do not have permission.") :base(msg, 403)
        {
            
        }
    }
}
