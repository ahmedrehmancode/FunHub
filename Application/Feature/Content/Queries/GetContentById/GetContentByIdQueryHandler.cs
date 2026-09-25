using Application.Interface;
using CEIS.Application.Common.Models;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Queries.GetContentById
{
    public class GetContentByIdQueryHandler : IRequestHandler<GetContentByIdQuery, Result<ContentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetContentByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ContentDto>> Handle(GetContentByIdQuery request, CancellationToken cancellationToken)
        {
            var content = await _unitOfWork.ContentRepository.GetByIdWithCategoryAsync(request.Id);

            if (content is null || !content.IsActive)
                throw new NotFoundException("Content not found.");

            content.ViewCount++;
            _unitOfWork.ContentRepository.Update(content);
            await _unitOfWork.SaveChangesAsync();

            var dto = new ContentDto
            {
                Id = request.Id,
                Title = content.Title,
                Description = content.Description,
                Type = content.Type,
                Genre = content.Genre,
                ThumbnailUrl = content.ThumbnailUrl,
                MediaUrl = content.MediaUrl,
                ReleaseDate = content.ReleaseDate,
                ViewCount = content.ViewCount,
                PopularityScore = content.PopularityScore,
                IsFeatured = content.IsFeatured,
                CategoryId = content.CategoryId,
                CategoryName = content.Category!.Name
            };

            return Result<ContentDto>.Success(dto);

        }
    }
}
