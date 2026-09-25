using Application.Interface;
using Application.Common.Models;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Commands.UpdateContent
{
    public class UpdateContentCommandHandler : IRequestHandler<UpdateContentCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateContentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            content.Title = request.Title;
            content.Description = request.Description;
            content.Type = request.Type;
            content.Genre = request.Genre;
            content.ThumbnailUrl = request.ThumbnailUrl;
            content.MediaUrl = request.MediaUrl;
            content.ReleaseDate = request.ReleaseDate;
            content.IsFeatured = request.IsFeatured;
            content.CategoryId = request.CategoryId;

            _unitOfWork.ContentRepository.Update(content);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Update Successfully!");
        }
    }
}
