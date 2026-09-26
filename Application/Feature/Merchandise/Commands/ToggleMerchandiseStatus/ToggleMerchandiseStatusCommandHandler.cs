using Application.Common.Models;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Merchandise.Commands.ToggleMerchandiseStatus
{
    public class ToggleMerchandiseStatusCommandHandler : IRequestHandler<ToggleMerchandiseStatusCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ToggleMerchandiseStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(ToggleMerchandiseStatusCommand request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWork.MerchandiseRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("Merchandise item not found.");

            item.IsActive = !item.IsActive;

            _unitOfWork.MerchandiseRepository.Update(item);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Successfully");

        }
    }
}
