using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class ContentFilterParams 
    {
        public int? CategoryId { get; set; }
        public ContentType? Type { get; set; }
        public int? ReleaseYear { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
        public string SortBy { get; set; } = "latest";   // latest | popular | alphabetical
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 12;
    }
}
