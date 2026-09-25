using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Queries
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? IconUrl { get; set; } = string.Empty;
        public string? BannerImageUrl { get; set; } = string.Empty;
    }
}
