using Application.Common.Models;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Bookmarks.Queries.GetMyBookmarks
{
    public class GetMyBookmarksQueryHandler : IRequestHandler<GetMyBookmarksQuery, Result<IEnumerable<BookmarkedContentDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetMyBookmarksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<BookmarkedContentDto>>> Handle(GetMyBookmarksQuery request, CancellationToken cancellationToken)
        {
            var bookmarks = await _unitOfWork.BookmarkRepository.GetByUserAsync(request.UserId);

            var dtos = bookmarks.Select(b => new BookmarkedContentDto
            {
                Id = b.Id,
                ContentId = b.ContentId,
                Title = b.Content!.Title,
                ThumbnailUrl = b.Content.ThumbnailUrl ?? "",
                Type = b.Content.Type,
                CreatedAt = b.CreatedAt
            });

            return Result<IEnumerable<BookmarkedContentDto>>.Success(dtos);
        }
    }
}
