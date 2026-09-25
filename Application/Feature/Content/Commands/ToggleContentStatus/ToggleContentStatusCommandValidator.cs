using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Commands.ToggleContentStatus
{
    public class ToggleContentStatusCommandValidator : AbstractValidator<ToggleContentStatusCommand>
    {
        public ToggleContentStatusCommandValidator()
        {
            RuleFor(x => x.ContentId)
                .GreaterThan(0)
                .WithMessage("Invalid Id");
        }
    }
}
