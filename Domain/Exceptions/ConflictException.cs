using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Domain.Exceptions
{
    public class ConflictException : AppException
    {
        public ConflictException(string msg) :base(msg, 409)
        {
            
        }
    }
}
