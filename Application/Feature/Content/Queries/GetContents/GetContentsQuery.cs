using Application.Common.Models;
using CEIS.Application.Common.Models;
using Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Queries.GetContents
{
    public record GetContentsQuery(ContentFilterParams Filters) : IRequest<Result<PagedResult<ContentListItemDto>>>
    {
    }
}
