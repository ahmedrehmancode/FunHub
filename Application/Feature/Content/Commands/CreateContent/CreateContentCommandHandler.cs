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

namespace Application.Feature.Content.Commands.CreateContent
{
    internal class CreateContentCommandHandler : IRequestHandler<CreateContentCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateContentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CreateContentCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId)
            ?? throw new NotFoundException("Category not found.");

            if (!category.IsActive)
                throw new ConflictException("Cannot add content to an inactive category.");

            if (await _unitOfWork.ContentRepository.ExistsByTitleInCategoryAsync(request.Title, request.CategoryId))
                throw new ConflictException("Content with this title already exists in this category.");

            var content = new Domain.Entity.Content
            {
                Title = request.Title,
                Description = request.Description,
                Type = request.Type,
                Genre = request.Genre,
                ThumbnailUrl = request.ThumbnailUrl,
                MediaUrl = request.MediaUrl,
                ReleaseDate = request.ReleaseDate,
                IsFeatured = request.IsFeatured,
                CategoryId = request.CategoryId
            };

            await _unitOfWork.ContentRepository.AddAsync(content);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Cantent Add Successfully!");
        }
    }
}
