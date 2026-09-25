using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Exceptions
{
    public class ValidationException : AppException
    {
        public List<string> Errors { get; set; } = new List<string>();
        public ValidationException(string msg) :base(msg, 422)
        {
            
        }

        public ValidationException(List<string> errors) :base("Validation Failed", 422)
        {
            Errors = errors;
        }
    }
}
