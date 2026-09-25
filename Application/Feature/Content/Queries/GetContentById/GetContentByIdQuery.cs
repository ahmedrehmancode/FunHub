using Application.Common.Models;
using Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Queries.GetContentById
{
    public class GetContentByIdQuery : IRequest<Result<ContentDto>>
    {
        public int Id { get; set; }
    }
}
