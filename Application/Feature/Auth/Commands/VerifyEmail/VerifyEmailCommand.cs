using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Auth.Commands.VerifyEmail
{
    public class VerifyEmailCommand : IRequest<Result<bool>>
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
