using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Queries
{
    public class ContentListItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public ContentType Type { get; set; }

        public string Genre { get; set; } = string.Empty;

        public string ThumbnailUrl { get; set; } = string.Empty;

        public DateTime? ReleaseDate { get; set; }

        public double PopularityScore { get; set; }

        public bool IsFeatured { get; set; }

        public string CategoryName { get; set; } = string.Empty;
    }
}
