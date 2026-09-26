using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Queries
{
    public class ContentDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ContentType Type { get; set; }

        public string? ThumbnailUrl { get; set; } = string.Empty;

        public string? MediaUrl { get; set; } = string.Empty;

        public DateOnly? ReleaseDate { get; set; }

        public int ViewCount { get; set; }

        public double PopularityScore { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;
    }
}
