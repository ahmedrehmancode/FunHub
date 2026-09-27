using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Feedback.Commands.SubmitFeedback
{
    internal class SubmitFeedbackCommandValidator : AbstractValidator<SubmitFeedbackCommand>
    {
        public SubmitFeedbackCommandValidator()
        {
            RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Please select a valid feedback type.");

            RuleFor(x => x.Message)
                .NotEmpty()
                .WithMessage("Feedback message is required.")
                .MaximumLength(1000)
                .WithMessage("Feedback message cannot exceed 1000 characters.");

            RuleFor(x => x.Star)
                .InclusiveBetween(1, 5)
                .WithMessage("Star rating must be between 1 and 5.");
        }
    }
}
