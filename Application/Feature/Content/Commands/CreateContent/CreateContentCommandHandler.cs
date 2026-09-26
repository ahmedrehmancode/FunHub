using Application.Interface;
using Application.Common.Models;
using Domain.Exceptions;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface.Servies;
using Domain.Enum;

namespace Application.Feature.Content.Commands.CreateContent
{
    public class CreateContentCommandHandler : IRequestHandler<CreateContentCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;
        public CreateContentCommandHandler(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Result<string>> Handle(CreateContentCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId)
            ?? throw new NotFoundException("Category not found.");

            if (!category.IsActive)
                throw new ConflictException("Cannot add content to an inactive category.");

            if (await _unitOfWork.ContentRepository.ExistsByTitleInCategoryAsync(request.Title, request.CategoryId))
                throw new ConflictException("Content with this title already exists in this category.");

            string? thumbnailUrl = null;
            if (request.ThumbnailFile is not null)
                thumbnailUrl = await _cloudinaryService.UploadImageAsync(request.ThumbnailFile, "fanhubplus/thumbnails");

            string? mediaUrl = null;

            if (request.MediaFile != null)
            {
                mediaUrl = request.Type switch
                {
                    ContentType.Image => await _cloudinaryService.UploadImageAsync(request.MediaFile, "fanhubplus/media"),
                    ContentType.Video => await _cloudinaryService.UploadVideoAsync(request.MediaFile, "fanhubplus/media"),
                    ContentType.Audio => await _cloudinaryService.UploadVideoAsync(request.MediaFile, "fanhubplus/media"),
                    _ => null
                };
            }

            var content = new Domain.Entity.Content
            {
                Title = request.Title,
                Description = request.Description,
                Type = request.Type,
                ThumbnailUrl = thumbnailUrl,
                MediaUrl = mediaUrl,
                ReleaseDate = request.ReleaseDate,
                CategoryId = request.CategoryId
            };

            await _unitOfWork.ContentRepository.AddAsync(content);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Cantent Add Successfully!");
        }
    }
}
