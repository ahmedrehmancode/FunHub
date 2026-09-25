using Application.Interface;
using Application.Common.Models;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Commands.ToggleCategoryStatus
{
    public class ToggleCategoryStatusCommandHandler : IRequestHandler<ToggleCategoryStatusCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ToggleCategoryStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(ToggleCategoryStatusCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.cId)
            ?? throw new NotFoundException("Category not found.");

            category.IsActive = !category.IsActive;
            category.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<string>.Success("Successfully updated!");
        }
    }
}
