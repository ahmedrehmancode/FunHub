using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class AuthResult
    {
        public bool Succeeded { get; set; }
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public IdentityGenderEnum? Gender { get; set; }
        public string? AvatarUrl { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
        public string? Token { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
