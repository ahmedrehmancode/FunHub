using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Feedback.Queries.GetMyFeedback
{
    public class GetMyFeedbackQuery : IRequest<Result<IEnumerable<FeedbackDto>>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
