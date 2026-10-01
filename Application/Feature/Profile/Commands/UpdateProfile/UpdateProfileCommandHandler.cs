using Application.Common.Models;
using Application.Interface.Servies;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Profile.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result<string>>
    {
        private readonly IIdentityService _identityService;
        private readonly ICloudinaryService _cloudinaryService;

        public UpdateProfileCommandHandler(
            IIdentityService identityService,
            ICloudinaryService cloudinaryService)
        {
            _identityService = identityService;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Result<string>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.FindUserByIdAsync(request.UserId);
            if (user == null)
                return Result<string>.Failure("User not found.");

            if (!string.IsNullOrWhiteSpace(request.FullName))
                user.FullName = request.FullName;

            if (!string.IsNullOrWhiteSpace(request.Bio))
                user.Bio = request.Bio;

            // Naya avatar upload hua ho to hi update karo
            if (request.Avatar != null)
            {
                var avatarUrl = await _cloudinaryService.UploadImageAsync(request.Avatar, "avatars");
                user.AvatarUrl = avatarUrl;
            }

            // Naya cover photo upload hua ho to hi update karo
            if (request.CoverPhoto != null)
            {
                var coverUrl = await _cloudinaryService.UploadImageAsync(request.CoverPhoto, "covers");
                user.CoverPhotoUrl = coverUrl;
            }

            var result = await _identityService.UpdateProfileAsync(user);
            if (!result.Succeeded)
                return Result<string>.Failure("Failed to update profile.");

            return Result<string>.Success("Profile updated successfully.");
        }
    }
}
