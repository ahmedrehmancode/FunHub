using Application.Common.Models;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Servies
{
    public interface IIdentityService
    {
        Task<AuthResult> RegisterAsync(
            string fullName,
            string email,
            string password,
            Domain.Enum.IdentityGenderEnum Gender,
            string role
            );

        Task<AuthResult> LoginAsync(string emailOrUsername, string Password);

        // Application/Interface/Servies/IIdentityService.cs
        Task<AuthResult> VerifyEmail(string token, string email);

        Task<User> FindUserByEmailAsync(string email);
        Task<AuthResult> UpdatePasswordAsync(string userId, string newPassword);

        Task<User> FindUserByIdAsync(string userId);

        Task<AuthResult> UpdateProfileAsync(User data);
    }
}
