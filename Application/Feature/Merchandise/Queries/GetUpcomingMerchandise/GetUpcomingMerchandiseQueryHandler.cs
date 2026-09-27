using Application.Common.Models;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Merchandise.Queries.GetUpcomingMerchandise
{
    public class GetUpcomingMerchandiseQueryHandler : IRequestHandler<GetUpcomingMerchandiseQuery, Result<IEnumerable<MerchandiseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUpcomingMerchandiseQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<MerchandiseDto>>> Handle(GetUpcomingMerchandiseQuery request, CancellationToken cancellationToken)
        {
            var items = await _unitOfWork.MerchandiseRepository.GetUpcomingAsync();

            var dtos = items.Select(m => new MerchandiseDto
            {
                Id = m.Id,
                Name = m.Name,
                Tag = m.Tag,
                ImageUrl = m.ImageUrl,
                IsUpcoming = m.IsUpcoming
            });

            return Result<IEnumerable<MerchandiseDto>>.Success(dtos);
        }
    }
}
