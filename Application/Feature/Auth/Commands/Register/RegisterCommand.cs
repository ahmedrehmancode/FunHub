using Application.Common.Models;
using Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<Result<string>>
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ComformPassword { get; set; } = string.Empty;
        public IdentityGenderEnum Gender { get; set; }
    }
}
