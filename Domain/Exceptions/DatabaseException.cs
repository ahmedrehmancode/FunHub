using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Domain.Exceptions
{
    public class DatabaseException : AppException
    {
        public DatabaseException(string msg = "A database error occurred. Please try again.") : base(msg, 500)
        {
            
        }
    }
}
