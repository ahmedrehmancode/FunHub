using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;      
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;        // sidebar/card icon
        public string BannerImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<Content> Contents { get; set; } = new List<Content>();
    }
}
