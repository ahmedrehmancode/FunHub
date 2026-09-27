using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Feedback : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public FeedbackType Type { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Star { get; set; }
    }
}
