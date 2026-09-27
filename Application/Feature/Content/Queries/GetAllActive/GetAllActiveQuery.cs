using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Queries.GetAllActive
{
    public class GetAllActiveQuery : IRequest<Result<IEnumerable<ContentDto>>>
    {
    }
}
