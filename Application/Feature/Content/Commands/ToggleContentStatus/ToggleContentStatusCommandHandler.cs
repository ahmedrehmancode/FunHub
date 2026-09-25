using Application.Interface;
using CEIS.Application.Common.Models;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Commands.ToggleContentStatus
{
    public class ToggleContentStatusCommandHandler : IRequestHandler<ToggleContentStatusCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ToggleContentStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(ToggleContentStatusCommand request, CancellationToken cancellationToken)
        {
            var content = await _unitOfWork.ContentRepository.GetByIdAsync(request.ContentId)
            ?? throw new NotFoundException("Content not found.");

            content.IsActive = !content.IsActive;
            content.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.ContentRepository.Update(content);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Successfully");
        }
    }
}
