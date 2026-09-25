// Application/Feature/Auth/Commands/VerifyEmail/VerifyEmailCommandHandler.cs
using Application.Common.Models;
using Application.Interface.Servies;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Feature.Auth.Commands.VerifyEmail
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, Result<bool>>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<VerifyEmailCommandHandler> _logger;

        public VerifyEmailCommandHandler(IIdentityService identityService, ILogger<VerifyEmailCommandHandler> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        public async Task<Result<bool>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.VerifyEmail(request.Token, request.Email);

            _logger.LogInformation("Email verification attempt for {Email}. Result: {Result}",
                request.Email, result.Succeeded ? "Success" : "Failed");
            if (result.Succeeded)
                return Result<bool>.Success(true, "Email verified successfully.");

            return Result<bool>.Failure(
                result.Errors.Any() ? result.Errors : new List<string> { "Email verification failed." });
        }
    }
}