using Application.Common.Models;
using Application.Interface;
using Application.Interface.Repositories;
using Application.Interface.Servies;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<string>>
    {
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;

        public ResetPasswordCommandHandler(
            IIdentityService identityService,
            IUnitOfWork unitOfWork)
        {
            _identityService = identityService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var findUserResult = await _identityService.FindUserByEmailAsync(request.Email);
            if (findUserResult == null)
                return Result<string>.Failure("User not found");

            var userId = findUserResult.Id;

            var tokenHash = Convert.ToBase64String(
                SHA256.HashData(Encoding.UTF8.GetBytes(request.Token)));

            var storedToken = await _unitOfWork.PasswordResetTokenRepository.GetValidTokenAsync(userId!, tokenHash);

            if (storedToken == null || storedToken.ExpiresAt < DateTime.UtcNow)
                return Result<string>.Failure("Invalid or expired token.");

            var updated = await _identityService.UpdatePasswordAsync(userId!, request.NewPassword);
            if (!updated.Succeeded)
                return Result<string>.Failure("Failed to update password.");

            storedToken.IsUsed = true;
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Password reset successful.");
        }
    }
}
