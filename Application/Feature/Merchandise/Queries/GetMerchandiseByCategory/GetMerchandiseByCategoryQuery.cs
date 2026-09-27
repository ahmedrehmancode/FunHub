using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Merchandise.Queries.GetMerchandiseByCategory
{
    public class GetMerchandiseByCategoryQuery : IRequest<Result<IEnumerable<MerchandiseDto>>>
    {
        public int CategoryId { get; set; }
    }
}
