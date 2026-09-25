using Application.Interface;
using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Queries.GetAllCategory
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, Result<IEnumerable<CategoryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllCategoryQueryHandler(IUnitOfWork unitOfWork)
        { 
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetAllAsync(cancellationToken);

            var dtos = category.Select(c => new CategoryDto
            {
                Id = c.Id,
                Description = c.Description,
                IconUrl = c.IconUrl,
                BannerImageUrl = c.BannerImageUrl,
                Name = c.Name,
            });

            return Result<IEnumerable<CategoryDto>>.Success(dtos);
        }
    }
}
