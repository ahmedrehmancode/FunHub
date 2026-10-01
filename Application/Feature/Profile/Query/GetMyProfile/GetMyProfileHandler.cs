using Application.Common.Models;
using Application.Interface;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Profile.Query.GetMyProfile
{
    internal class GetMyProfileHandler : IRequestHandler<GetMyProfileQuery, Result<GetMyProfileDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMyProfileHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetMyProfileDTO>> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);

            if (profile == null)
            {
                return Result<GetMyProfileDTO>.Failure("User not found.");
            }

            var profileDto = new GetMyProfileDTO
            {
                FullName = profile.FullName,
                UserName = profile.UserName,
                Bio = profile.Bio,
                Gender = profile.Gender
            };

            return Result<GetMyProfileDTO>.Success(profileDto);
        }
    }
}

                
