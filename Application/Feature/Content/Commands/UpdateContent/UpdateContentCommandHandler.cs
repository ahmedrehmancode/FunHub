using Application.Interface;
using Application.Common.Models;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface.Servies;
using Domain.Enum;

namespace Application.Feature.Content.Commands.UpdateContent
{
    public class UpdateContentCommandHandler : IRequestHandler<UpdateContentCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;
        public UpdateContentCommandHandler(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Result<string>> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
        {
            var content = await _unitOfWork.ContentRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("Content not found.");

            if (request.CategoryId != content.CategoryId)
            {
                var newCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId)
                    ?? throw new NotFoundException("Target category not found.");

                if (!newCategory.IsActive)
                    throw new ConflictException("Cannot move content to an inactive category.");
            }

            if (request.ThumbnailFile != null)
            {
                content.ThumbnailUrl = await _cloudinaryService.UploadImageAsync(
                    request.ThumbnailFile, "fanhubplus/thumbnails");
            }

            if (request.MediaFile != null)
            {
                content.MediaUrl = request.Type switch
                {
                    ContentType.Image => await _cloudinaryService.UploadVideoAsync(
                        request.MediaFile, "fanhubplus/media"),
                    ContentType.Video or ContentType.Audio => await _cloudinaryService.UploadVideoAsync(
                        request.MediaFile, "fanhubplus/media"),
                    _ => content.MediaUrl
                };
            }

            content.Title = request.Title;
            content.Description = request.Description;
            content.Type = request.Type;
            content.ReleaseDate = request.ReleaseDate;
            content.CategoryId = request.CategoryId;

            _unitOfWork.ContentRepository.Update(content);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Update Successfully!");
        }
    }
}
