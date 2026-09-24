using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.emailOrUsername)
                .NotEmpty().WithMessage("Email or Username is required.");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
