using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Queries.GetContentById
{
    public class GetContentByIdQueryValidator : AbstractValidator<GetContentByIdQuery>
    {
        public GetContentByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Invalid Id");
        }
    }
}
