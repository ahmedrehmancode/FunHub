using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Bookmarks.Queries.GetMyBookmarks
{
    public class GetMyBookmarksQuery : IRequest<Result<IEnumerable<BookmarkedContentDto>>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
