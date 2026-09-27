using Application.Common.Models;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Feedback.Queries.GetMyFeedback
{
    public class GetMyFeedbackQueryHandler : IRequestHandler<GetMyFeedbackQuery, Result<IEnumerable<FeedbackDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetMyFeedbackQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<FeedbackDto>>> Handle(GetMyFeedbackQuery request, CancellationToken cancellationToken)
        {
            var feedbacks = await _unitOfWork.FeedbackRepository.GetByUserAsync(request.UserId);

            var dtos = feedbacks.Select(f => new FeedbackDto
            {
                Id = f.Id,
                Type = f.Type,
                Message = f.Message,
                Star = f.Star,
                CreatedAt = f.CreatedAt
            });

            return Result<IEnumerable<FeedbackDto>>.Success(dtos);
        }
    }
}
