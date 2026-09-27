using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Commands.SubmitContentUser
{
    public class SubmitContentUserCommandValidator : AbstractValidator<SubmitContentUserCommand>
    {
        private static readonly string[] ImageExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        private const long MaxThumbnailBytes = 30 * 1024 * 1024;
        public SubmitContentUserCommandValidator()
        {
            RuleFor(x => x.SubmittedByUserId)
                .NotEmpty()
                .WithMessage("Submitted by user ID is required.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(200)
                .WithMessage("Title must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(10000)
                .WithMessage("Description must not exceed 10000 characters.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage("Category ID must be greater than 0.");

            When(x => x.ThumbnailFile != null, () =>
            {
                RuleFor(x => x.ThumbnailFile)
                    .Must(f => ImageExtensions.Contains(
                        Path.GetExtension(f!.FileName).ToLowerInvariant()))
                    .WithMessage("Thumbnail must be a JPG, JPEG, PNG, or WEBP image.")
                    .Must(f => f!.Length <= MaxThumbnailBytes)
                    .WithMessage("Thumbnail size must not exceed 30 MB.");
            });
        }
    }
}
