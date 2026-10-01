using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class User
    {
        public string Id { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string? PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public IdentityGenderEnum Gender { get; set; }

        public string? AvatarUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }

        public string? Bio { get; set; }

        public string? CoverPhotoUrl { get; set; }
    }
}
