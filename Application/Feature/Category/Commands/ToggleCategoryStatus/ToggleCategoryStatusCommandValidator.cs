using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Commands.ToggleCategoryStatus
{
    public class ToggleCategoryStatusCommandValidator : AbstractValidator<ToggleCategoryStatusCommand>
    {
        public ToggleCategoryStatusCommandValidator()
        {
            RuleFor(x => x.cId)
                .GreaterThan(0)
                .WithMessage("invalid id");
        }
    }
}
