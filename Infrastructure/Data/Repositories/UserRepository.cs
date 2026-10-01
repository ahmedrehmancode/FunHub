using Application.Interface.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<User?> GetByIdAsync(string userId)
        {
            var user = await _dbContext.Users
                .Where(u => u.Id == userId)
                .ProjectTo<User>(_mapper.ConfigurationProvider)   // AutoMapper projection — SQL level pe hi selective columns
                .FirstOrDefaultAsync();

            return user;
        }
    }
}