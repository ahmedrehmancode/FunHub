using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Merchandise.Commands.CreateMerchandise
{
    public class CreateMerchandiseCommand : IRequest<Result<string>>
    {
        public string Name { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public bool IsUpcoming { get; set; }
        public int CategoryId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
