using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Auth.Commands.Login
{
    public class LoginCommand : IRequest<Result<AuthResult>>
    {
        public string emailOrUsername {  get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
