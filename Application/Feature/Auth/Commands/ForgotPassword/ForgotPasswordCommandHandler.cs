using Application.Interface;
using Application.Interface.Repositories;
using Application.Interface.Servies;
using Domain.Entity;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        private readonly IIdentityService _identityService;
 
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configruation;
        private readonly ILogger<ForgotPasswordCommandHandler> _logger;

        public ForgotPasswordCommandHandler(
            IIdentityService identityService,
            IEmailService emailService,
            IUnitOfWork unitOfWork,
            IConfiguration config,
            ILogger<ForgotPasswordCommandHandler> logger)
        {
            _identityService = identityService;  
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _configruation = config;
            _logger = logger;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" ForgotPasswordCommadn: Handling ForgotPasswordCommand for email: {Email}", request.Email);
            var (userId, email) = await _identityService.FindUserByEmailAsync(request.Email);

            // User enumeration se bachne ke liye — na mile to bhi silently return karo
            if (userId == null)
                return Unit.Value;
            
            _logger.LogInformation(" ForgotPasswordCommand: User found for email: {Email}", request.Email);

            var rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            _logger.LogInformation(" ForgotPasswordCommand: Generated raw token for userId: {UserId}", userId);
            var tokenHash = Convert.ToBase64String(
                SHA256.HashData(Encoding.UTF8.GetBytes(rawToken)));

            var resetToken = new PasswordResetToken
            {
                UserId = userId,
                TokenHash = tokenHash,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30),
                IsUsed = false
            };
            _logger.LogInformation(" ForgotPasswordCommand: Created PasswordResetToken entity for userId: {UserId}", userId);
            await _unitOfWork.PasswordResetTokenRepository.AddAsync(resetToken);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation(" ForgotPasswordCommand: Saved PasswordResetToken to database for userId: {UserId}", userId);

            var baseUrl = _configruation["AppSettings:BaseUrl"];
            var resetLink = $"{baseUrl}/api/Auth/reset-password?email={Uri.EscapeDataString(email!)}&token={Uri.EscapeDataString(rawToken)}";
            _logger.LogInformation(" ForgotPasswordCommand: Generated reset link for userId: {UserId}", userId);
            await _emailService.SendPasswordResetEmailAsync(email!, resetLink);


            return Unit.Value;
        }
    }
}
