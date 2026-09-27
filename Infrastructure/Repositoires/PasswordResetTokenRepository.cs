using Application.Interface.Repositories;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositoires
{
    public class PasswordResetTokenRepository : IPasswordResetTokenRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<PasswordResetTokenRepository> _logger;

        public PasswordResetTokenRepository(ApplicationDbContext dbContext, ILogger<PasswordResetTokenRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddAsync(PasswordResetToken token)
        {
            _logger.LogInformation("PasswordResetTokenRepository: Adding new password reset token for userId: {UserId}", token.UserId);
            await _dbContext.PasswordResetTokens.AddAsync(token);
            _logger.LogInformation("PasswordResetTokenRepository: Successfully added password reset token for userId: {UserId}", token.UserId);
        }

        public async Task<PasswordResetToken?> GetValidTokenAsync(string userId, string tokenHash)
        {
            _logger.LogInformation("PasswordResetTokenRepository: Retrieving valid password reset token for userId: {UserId}", userId);
            return await _dbContext.PasswordResetTokens
                .Where(t => t.UserId == userId && t.TokenHash == tokenHash && !t.IsUsed)
                .OrderByDescending(t => t.Id)
                .FirstOrDefaultAsync();
        }
    }
}
