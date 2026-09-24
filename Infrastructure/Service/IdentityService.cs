using Application.Interface.Servies;
using CEIS.Application.Common.Models;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public IdentityService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<AuthResult> LoginAsync(string emailOrUsername, string password)
        {
            var loginUser = await _userManager.FindByEmailAsync(emailOrUsername)
                ?? await _userManager.FindByNameAsync(emailOrUsername);

            if (loginUser == null)
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new List<string> { "Invalid email or password." }
                };

            var isPasswordValid = await _userManager.CheckPasswordAsync(loginUser, password);

            if (!isPasswordValid)
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new List<string> { "Invalid email or password." }
                };

            var roles = await _userManager.GetRolesAsync(loginUser);

            return new AuthResult
            {
                Succeeded = true,
                UserId = loginUser.Id,
                Email = loginUser.Email,
                Gender = loginUser.Gender,
                FullName = loginUser.FullName,
                Roles = roles
            };
        }

        public async Task<AuthResult> RegisterAsync(string fullName, string userName, string email, string password, Domain.Enum.IdentityGenderEnum Gender, string role)
        {
            var existingUserEmail = await _userManager.FindByEmailAsync(email);
            if (existingUserEmail != null)
            {
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new List<string> { "An account with this email already exists." }
                };
            }

            var existingUser = await _userManager.FindByNameAsync(userName);
            if (existingUser != null)
            {
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new List<string> { "This username is already taken." }
                };
            }

            var user = new ApplicationUser
            {
                FullName = fullName,
                Email = email,
                UserName = userName,
                Gender = Gender,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };

            await _userManager.AddToRoleAsync(user, role);

            return new AuthResult
            {
                Succeeded = true,
            };
        }
    }
}
