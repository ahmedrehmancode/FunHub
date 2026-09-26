using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class MerchandiseItem : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string Tag { get; set; } = string.Empty;
        public bool IsUpcoming { get; set; }
        public int ViewCount { get; set; } = 0;
        public bool IsActive { get; set; } = true;

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
