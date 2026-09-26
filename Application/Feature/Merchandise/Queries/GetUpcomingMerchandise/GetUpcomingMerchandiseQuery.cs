using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Merchandise.Queries.GetUpcomingMerchandise
{
    public class GetUpcomingMerchandiseQuery : IRequest<Result<IEnumerable<MerchandiseDto>>>
    {

    }
}
