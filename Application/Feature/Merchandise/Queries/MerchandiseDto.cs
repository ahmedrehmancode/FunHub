using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Merchandise.Queries
{
    public class MerchandiseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public bool IsUpcoming { get; set; }
    }
}
