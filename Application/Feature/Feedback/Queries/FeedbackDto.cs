using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Feedback.Queries
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public FeedbackType Type { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Star { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
