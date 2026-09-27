using Application.Interface.Servies;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Application.Feature.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string>>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<RegisterCommandHandler> _logger;
        public RegisterCommandHandler(IIdentityService identityService, IJwtTokenGenerator jwtTokenGenerator, ILogger<RegisterCommandHandler> logger)
        {
            _identityService = identityService;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.RegisterAsync(
                request.FullName,
                request.Email,
                request.Password,
                request.Gender,
                "RegisterUser"
            );


            _logger.LogInformation("User registration attempt for {Email}. Result: {Result}", request.Email, result.Succeeded ? "Success" : "Failure");


            if (!result.Succeeded)
                return Result<string>.Failure(result.Errors);


            return Result<string>.Success("Account created successfully. Please check your email to verify your account.");
        }
    }
}
