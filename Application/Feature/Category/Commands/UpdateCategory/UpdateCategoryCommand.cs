using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<Result<Unit>>
    {
        public int cId { get; set; }
        public string? Description { get; set; } = string.Empty;
        public string? IconUrl { get; set; } = string.Empty;
        public string? BannerImageUrl { get; set; } = string.Empty;
    }
}
