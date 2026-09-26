using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Bookmarks.Queries
{
    public class BookmarkedContentDto
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public ContentType Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
