using Application.Interface.Servies;
using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResult>>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginCommandHandler(IIdentityService identityService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _identityService = identityService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result<AuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.LoginAsync(request.emailOrUsername, request.password);

            if (!result.Succeeded)
                return Result<AuthResult>.Failure(result.Errors);

            var tokken = await _jwtTokenGenerator.GenerateTokenAsync(result.UserId!, result.FullName!, result.Gender.ToString()!, result.Roles);

            result.Token = tokken;

            return Result<AuthResult>.Success(result);
        }
    }
}
