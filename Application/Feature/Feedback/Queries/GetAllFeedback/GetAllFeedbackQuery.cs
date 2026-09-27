using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Feedback.Queries.GetAllFeedback
{
    public class GetAllFeedbackQuery : IRequest<Result<IEnumerable<FeedbackDto>>>
    {
    }
}
