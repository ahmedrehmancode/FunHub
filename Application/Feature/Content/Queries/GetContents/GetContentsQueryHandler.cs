using Application.Common.Models;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Queries.GetContents
{
    public class GetContentsQueryHandler : IRequestHandler<GetContentsQuery, Result<PagedResult<ContentListItemDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetContentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PagedResult<ContentListItemDto>>> Handle(GetContentsQuery request, CancellationToken cancellationToken)
        {
            var paged = await _unitOfWork.ContentRepository.GetFilteredAsync(request.Filters);

            var dtos = paged.Items!.Select(c => new ContentListItemDto
            {
                Id = c.Id,
                Title = c.Title,
                Type = c.Type,
                ThumbnailUrl = c.ThumbnailUrl,
                ReleaseDate = c.ReleaseDate,
                PopularityScore = c.PopularityScore,
                CategoryName = c.Category!.Name
            });

            var result = new PagedResult<ContentListItemDto>
            {
                Items = dtos,
                TotalCount = paged.TotalCount,
                PageNumber = paged.PageNumber,
                PageSize = paged.PageSize
            };

            return Result<PagedResult<ContentListItemDto>>.Success(result);
        }
    }
}
