using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Merchandise.Commands.ToggleMerchandiseStatus
{
    public class ToggleMerchandiseStatusCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
    }
}
