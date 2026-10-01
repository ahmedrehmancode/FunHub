using Domain.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Profile.Commands.UpdateProfile
{
    public class UpdateProfileRequestDTO
    {
        public string? FullName { get; set; }
        public string? Bio { get; set; }

        public string? Username { get; set; } // optional username update

        //public IdentityGenderEnum? Gender { get; set; } // optional gender update


    }
}
