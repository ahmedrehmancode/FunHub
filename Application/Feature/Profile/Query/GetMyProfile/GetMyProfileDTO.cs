using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Profile.Query.GetMyProfile
{
    public class GetMyProfileDTO
    {
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string? Bio { get; set; } = string.Empty;
        public IdentityGenderEnum Gender { get; set; }
    }
}
