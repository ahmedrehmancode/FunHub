using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Queries.GetAllCategory
{
    public class GetAllCategoryQuery : IRequest<Result<IEnumerable<CategoryDto>>>
    {
    }
}
