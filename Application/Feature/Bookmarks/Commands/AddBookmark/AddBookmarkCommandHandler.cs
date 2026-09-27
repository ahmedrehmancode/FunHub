using Application.Common.Models;
using Application.Interface;
using Domain.Entity;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Bookmarks.Commands.AddBookmark
{
    public class AddBookmarkCommandHandler : IRequestHandler<AddBookmarkCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddBookmarkCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(AddBookmarkCommand request, CancellationToken cancellationToken)
        {
            var content = await _unitOfWork.ContentRepository.GetByIdAsync(request.ContentId)
            ?? throw new NotFoundException("Content not found.");

            if (!content.IsActive)
                throw new ConflictException("Cannot bookmark inactive content.");

            if (await _unitOfWork.BookmarkRepository.ExistsAsync(request.UserId, request.ContentId))
                throw new ConflictException("This content is already bookmarked.");

            var bookmark = new Bookmark
            {
                UserId = request.UserId,
                ContentId = request.ContentId
            };

            await _unitOfWork.BookmarkRepository.AddAsync(bookmark);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Successfully Added");
        }
    }
}
