using Application.Interface;
using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Commands.UpdateContent
{
    public class UpdateContentCommandValidator : IRequestHandler<UpdateContentCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateContentCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Result<string>> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
