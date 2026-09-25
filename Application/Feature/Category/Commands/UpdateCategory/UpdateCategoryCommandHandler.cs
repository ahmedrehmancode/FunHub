using Application.Interface;
using CEIS.Application.Common.Models;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.cId, cancellationToken)
            ?? throw new NotFoundException("Category not found.");

            category.Description = request.Description!;
            category.IconUrl = request.IconUrl!;
            category.BannerImageUrl = request.BannerImageUrl!;
            category.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
