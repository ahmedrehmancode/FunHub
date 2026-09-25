using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string msg) :base(msg, 404)
        {
            
        }

        public NotFoundException(string entityName, object id) :base($"{entityName} with id '{id}' was not found.", 404)
        {
            
        }
    }
}
