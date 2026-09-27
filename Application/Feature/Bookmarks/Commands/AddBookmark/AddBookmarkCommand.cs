using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Bookmarks.Commands.AddBookmark
{
    public class AddBookmarkCommand : IRequest<Result<string>>
    {
        public string UserId { get; set; } = string.Empty; 
        public int ContentId { get; set; }
    }
}
