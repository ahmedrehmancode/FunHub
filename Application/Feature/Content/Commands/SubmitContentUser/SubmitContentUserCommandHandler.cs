using Application.Common.Models;
using Application.Interface;
using Application.Interface.Servies;
using Domain.Enum;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Commands.SubmitContentUser
{
    public class SubmitContentUserCommandHandler : IRequestHandler<SubmitContentUserCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;
        public SubmitContentUserCommandHandler(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Result<string>> Handle(SubmitContentUserCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId)
            ?? throw new NotFoundException("Category not found.");

            if (!category.IsActive)
                throw new ConflictException("Cannot submit content to an inactive category.");

            string? thumbnailUrl = null;
            if (request.ThumbnailFile != null)
                thumbnailUrl = await _cloudinaryService.UploadImageAsync(request.ThumbnailFile, "fanhubplus/thumbnails");

            var content = new Domain.Entity.Content
            {
                Title = request.Title,
                Description = request.Description,
                Type = ContentType.Article,       // user submissions hamesha Article type
                ThumbnailUrl = thumbnailUrl,
                CategoryId = request.CategoryId,
                SubmittedByUserId = request.SubmittedByUserId,
                IsActive = false                  // pending approval — public list mein nahi dikhega
            };

            await _unitOfWork.ContentRepository.AddAsync(content);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Successfully submit");
        }
    }
}
