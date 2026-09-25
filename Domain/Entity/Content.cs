using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Content : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Domain.Enum.ContentType Type { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public string MediaUrl { get; set; } = string.Empty;  
        public DateTime? ReleaseDate { get; set; }
        public int ViewCount { get; set; } = 0;
        public double PopularityScore { get; set; } = 0;
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; } = true;

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
