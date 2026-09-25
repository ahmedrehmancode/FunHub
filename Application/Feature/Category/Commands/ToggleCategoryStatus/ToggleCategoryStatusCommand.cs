using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Commands.ToggleCategoryStatus
{
    public class ToggleCategoryStatusCommand : IRequest<Result<string>>
    {
        public int cId { get; set; }
    }
}
