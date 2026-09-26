using Application.Common.Models;
using Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Feedback.Commands.SubmitFeedback
{
    public class SubmitFeedbackCommand: IRequest<Result<string>>
    {
        public string UserId { get; set; } = string.Empty;
        public FeedbackType Type { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Star { get; set; }
    }
}
