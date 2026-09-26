using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Bookmark : BaseEntity
    {
        public string UserId { get; set; } = string.Empty; 
        public int ContentId { get; set; }
        public Content? Content { get; set; }
    }
}
