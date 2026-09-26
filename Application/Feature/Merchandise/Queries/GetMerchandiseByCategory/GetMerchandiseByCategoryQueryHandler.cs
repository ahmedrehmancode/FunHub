using Application.Common.Models;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Merchandise.Queries.GetMerchandiseByCategory
{
    public class GetMerchandiseByCategoryQueryHandler : IRequestHandler<GetMerchandiseByCategoryQuery, Result<IEnumerable<MerchandiseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetMerchandiseByCategoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<MerchandiseDto>>> Handle(GetMerchandiseByCategoryQuery request, CancellationToken cancellationToken)
        {
            var items = await _unitOfWork.MerchandiseRepository.GetByCategoryAsync(request.CategoryId);

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
