using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Profile.Query.GetMyProfile
{
    public class GetMyProfileQuery : IRequest<Result<GetMyProfileDTO>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
