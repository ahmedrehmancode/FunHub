using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Queries.GetActiveCategories
{
    public class GetActiveCategoryQuery : IRequest<Result<IEnumerable<CategoryDto>>>
    {
    }
}
