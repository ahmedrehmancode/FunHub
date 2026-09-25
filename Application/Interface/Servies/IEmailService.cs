using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Servies
{
    public interface IEmailService
    {
        Task<bool> SendVerificationEmailAsync(string email, string verificationLink);
    }
}
