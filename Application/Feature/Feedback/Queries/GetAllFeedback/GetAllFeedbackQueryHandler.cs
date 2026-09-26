using Application.Common.Models;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Feedback.Queries.GetAllFeedback
{
    public class GetAllFeedbackQueryHandler : IRequestHandler<GetAllFeedbackQuery, Result<IEnumerable<FeedbackDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllFeedbackQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<FeedbackDto>>> Handle(GetAllFeedbackQuery request, CancellationToken cancellationToken)
        {
            var feedbacks = await _unitOfWork.FeedbackRepository.GetAllAsync();

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
