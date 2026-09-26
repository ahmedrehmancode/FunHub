using Application.Common.Models;
using Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Commands.CreateContent
{
    public class CreateContentCommand : IRequest<Result<string>>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ContentType Type { get; set; }
        public int CategoryId { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public IFormFile? ThumbnailFile { get; set; } = null!;
        public IFormFile? MediaFile { get; set; }
    }
}
