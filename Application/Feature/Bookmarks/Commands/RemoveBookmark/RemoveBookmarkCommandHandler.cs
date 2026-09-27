using Application.Common.Models;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Bookmarks.Commands.RemoveBookmark
{
    public class RemoveBookmarkCommandHandler : IRequestHandler<RemoveBookmarkCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RemoveBookmarkCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(RemoveBookmarkCommand request, CancellationToken cancellationToken)
        {
            var bookmark = await _unitOfWork.BookmarkRepository.GetByUserAndContentAsync(request.UserId, request.ContentId)
            ?? throw new NotFoundException("Bookmark not found.");

            _unitOfWork.BookmarkRepository.Delete(bookmark);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Delete successfully");
        }
    }
}
