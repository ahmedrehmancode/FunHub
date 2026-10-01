using Application.Common.Models;
using Application.Interface.Servies;
using AutoMapper;
using Domain.Entity;
using Domain.Exceptions;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Infrastructure.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configruation;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IdentityService> _logger;
        private readonly IMapper _mapper;
        public IdentityService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration iconfigruation, IEmailService emailService, ILogger<IdentityService> logger, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _configruation = iconfigruation;
            _emailService = emailService;
            _logger = logger;
            _mapper = mapper;
        }
        // LoginAsync method to authenticate a user based on email/username and password
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

            // Check if the provided password is valid for the found user

            var isPasswordValid = await _userManager.CheckPasswordAsync(loginUser, password);

            if (!isPasswordValid)
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new List<string> { "Invalid email or password." }
                };

            if (!loginUser.EmailConfirmed)
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new List<string> { "Please verify your email before logging in." }
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

        // RegisterAsync method to create a new user account

        public async Task<AuthResult> RegisterAsync(string fullName, string email, string password, Domain.Enum.IdentityGenderEnum Gender, string role)
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

            var user = new ApplicationUser
            {
                FullName = fullName,
                Email = email,
                UserName = email,
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



            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            _logger.LogInformation("IdentityService : generated email confirmation token for {Email}", user.Email);
            var baseUrl = _configruation["AppSettings:BaseUrl"];
            _logger.LogInformation("IdentityService : BaseUrl from configuration is {BaseUrl}", baseUrl);
            var link = $"{baseUrl}/api/Auth/verify-email" +
                       $"?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(user.Email!)}";
            _logger.LogInformation("IdentityService : Verification link generated for {Email}: {Link}", user.Email, link);
            try
            {
                _logger.LogInformation("IdentityService : User created successfully. Sendting email to EmailSender for {Email}", user.Email);
                var emailResult = await _emailService.SendVerificationEmailAsync(user.Email!, link);
                if (!emailResult)
                {
                    _logger.LogInformation("IdentityService : Email sending failed for {Email}. Returning AuthResult with error message.", user.Email);
                    return new AuthResult
                    {
                        Succeeded = true,
                        Errors = new() { "Account created but verification email could not be sent. Please try again later." }
                    };
                }
                else
                {
                    _logger.LogInformation("IdentityService : Email sent successfully to {Email}. Returning AuthResult with success.", user.Email);
                    return new AuthResult
                    {
                        Succeeded = true

                    };
                }

            }
            catch (AppException)
            {


                return new AuthResult
                {
                    Succeeded = true,
                    Errors = new() { "Account created but verification email could not be sent." }
                };
            }


        }

        public async Task<AuthResult> VerifyEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new List<string> { "User not found" }
                };
            }

            if (user.EmailConfirmed)
            {
                return new AuthResult
                {
                    Succeeded = true,
                    UserId = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Gender = user.Gender
                };
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return new AuthResult
                {
                    Succeeded = true,
                    UserId = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Gender = user.Gender
                };
            }

            return new AuthResult
            {
                Succeeded = false,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }
        // FindUserByEmailAsync method to retrieve a user's ID and email based on their email address
        public async Task<User> FindUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new AppException("User not found");
            }
            return _mapper.Map<User>(user); 
                
            }

        

        // FindUserByIdAsync method to retrieve a user based on their ID

        public async Task<User> FindUserByIdAsync(string userId)
        {
            var CurrentUser = await _userManager.FindByIdAsync(userId);
            if (CurrentUser == null)
            {
                throw new AppException("User not found");
            }
            User user = _mapper.Map<User>(CurrentUser);
            return user;
        }


        // UpdatePasswordAsync method to update a user's password
        public async Task<AuthResult> UpdatePasswordAsync(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new AuthResult { Succeeded = false, Errors = new List<string> { "User not found." } };

            var hasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = hasher.HashPassword(user, newPassword);

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? new AuthResult { Succeeded = true } : new AuthResult { Succeeded = false, Errors = new List<string> { "Failed to update password." } };
        }
        // updateProfileAsync method to update a user's profile information
        public async Task<AuthResult> UpdateProfileAsync(User data)
        {
            var user = await _userManager.FindByIdAsync(data.Id);
            if (user == null)
                return new AuthResult { Succeeded = false, Errors = new List<string> { "User not found." } };

            if (!string.IsNullOrWhiteSpace(data.FullName))
                user.FullName = data.FullName;

            if (!string.IsNullOrWhiteSpace(data.Bio))
                user.Bio = data.Bio;

            if (!string.IsNullOrWhiteSpace(data.AvatarUrl))
                user.AvatarUrl = data.AvatarUrl;

            if (!string.IsNullOrWhiteSpace(data.CoverPhotoUrl))
                user.CoverPhotoUrl = data.CoverPhotoUrl;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? new AuthResult { Succeeded = true } : new AuthResult { Succeeded = false, Errors = new List<string> { "Failed to update profile." } };
        }
    }
}
