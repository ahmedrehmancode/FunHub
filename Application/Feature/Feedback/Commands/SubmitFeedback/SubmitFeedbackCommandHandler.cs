using Application.Common.Models;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Feedback.Commands.SubmitFeedback
{
    public class SubmitFeedbackCommandHandler : IRequestHandler<SubmitFeedbackCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubmitFeedbackCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(SubmitFeedbackCommand request, CancellationToken cancellationToken)
        {
             
            var feedback = new Domain.Entity.Feedback
            {
                UserId = request.UserId,
                Type = request.Type,
                Message = request.Message,
                Star = request.Star
            };

            await _unitOfWork.FeedbackRepository.AddAsync(feedback);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Successfully Submeted");
        }
    }
}
