using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Profile.Commands.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<Result<string>>
    {
        public string UserId { get; set; } = string.Empty;
        public string? FullName { get; set; }
        
        public string? Username { get; set; } // optional username update

        public string? Bio { get; set; }
        public IFormFile? Avatar { get; set; }        // naya avatar file (optional)
        public IFormFile? CoverPhoto { get; set; }     // naya cover photo file (optional)
    }
}
