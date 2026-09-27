using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Commands.SubmitContentUser
{
    public class SubmitContentUserCommand : IRequest<Result<string>>
    {
        public string SubmittedByUserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public IFormFile? ThumbnailFile { get; set; }
    }
}
