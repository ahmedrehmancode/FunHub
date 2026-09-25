using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Commands.ToggleContentStatus
{
    public class ToggleContentStatusCommand : IRequest<Result<string>>
    {
        public int ContentId { get; set; }
    }
}
