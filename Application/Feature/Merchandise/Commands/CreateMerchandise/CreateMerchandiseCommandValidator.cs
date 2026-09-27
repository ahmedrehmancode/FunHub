using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Merchandise.Commands.CreateMerchandise
{
    public class CreateMerchandiseCommandValidator : AbstractValidator<CreateMerchandiseCommand>
    {
        private static readonly string[] ImageExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        private const long MaxImageBytes = 30 * 1024 * 1024;
        public CreateMerchandiseCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
            RuleFor(x => x.Tag).NotEmpty().MaximumLength(50);
            RuleFor(x => x.CategoryId).GreaterThan(0);

            When(x => x.ImageFile is not null, () =>
            {
                RuleFor(x => x.ImageFile)
                    .Must(f => ImageExtensions.Contains(Path.GetExtension(f!.FileName).ToLowerInvariant()))
                    .WithMessage("Image sirf jpg, jpeg, png ya webp ho sakti hai.")
                    .Must(f => f!.Length <= MaxImageBytes).WithMessage("Image 5MB se zyada nahi honi chahiye.");
            });
        }
    }
}
