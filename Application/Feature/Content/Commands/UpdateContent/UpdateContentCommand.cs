using Application.Common.Models;
using Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Commands.UpdateContent
{
    public class UpdateContentCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ContentType Type { get; set; }

        public string Genre { get; set; } = string.Empty;

        public string ThumbnailUrl { get; set; } = string.Empty;

        public string MediaUrl { get; set; } = string.Empty;

        public DateTime? ReleaseDate { get; set; }

        public bool IsFeatured { get; set; }

        public int CategoryId { get; set; }
    }
}
