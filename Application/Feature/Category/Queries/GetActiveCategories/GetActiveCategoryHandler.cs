using Application.Interface;
using CEIS.Application.Common.Models;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Queries.GetActiveCategories
{
    public class GetActiveCategoryHandler : IRequestHandler<GetActiveCategoryQuery, Result<IEnumerable<CategoryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetActiveCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<CategoryDto>>> Handle(GetActiveCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.CategoryRepository.GetActiveCategoriesAsync();

            var dtos = categories.Select(c => new CategoryDto
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
