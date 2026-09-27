using Application.Common.Models;
using Application.Interface;
using Application.Interface.Servies;
using Domain.Entity;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Merchandise.Commands.CreateMerchandise
{
    public class CreateMerchandiseCommandHandler : IRequestHandler<CreateMerchandiseCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;
        public CreateMerchandiseCommandHandler(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Result<string>> Handle(CreateMerchandiseCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId)
            ?? throw new NotFoundException("Category not found.");

            if (!category.IsActive)
                throw new ConflictException("Cannot add merchandise to an inactive category.");

            string? imageUrl = null;
            if (request.ImageFile is not null)
                imageUrl = await _cloudinaryService.UploadImageAsync(request.ImageFile, "fanhubplus/merchandise");

            var item = new MerchandiseItem
            {
                Name = request.Name,
                Tag = request.Tag,
                IsUpcoming = request.IsUpcoming,
                ImageUrl = imageUrl,
                CategoryId = request.CategoryId
            };

            await _unitOfWork.MerchandiseRepository.AddAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Added successfully");
        }
    }
}
