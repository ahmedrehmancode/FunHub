using Application.Common.Models;
using Application.Interface;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Queries.GetAllActive
{
    public class GetAllActiveQueryHandler : IRequestHandler<GetAllActiveQuery, Result<IEnumerable<ContentDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllActiveQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<ContentDto>>> Handle(GetAllActiveQuery request, CancellationToken cancellationToken)
        {
            var content = await _unitOfWork.ContentRepository.GetAllActiveContentAsync();

            var dto = content.Select(content => new ContentDto
            {
                Id = content.Id,
                Title = content.Title,
                Description = content.Description,
                Type = content.Type,
                ThumbnailUrl = content.ThumbnailUrl,
                MediaUrl = content.MediaUrl,
                ReleaseDate = content.ReleaseDate,
                ViewCount = content.ViewCount,
                PopularityScore = content.PopularityScore,
                CategoryId = content.CategoryId,
                CategoryName = content.Category!.Name
            });

            return Result<IEnumerable<ContentDto>>.Success(dto);
        }
    }
}
