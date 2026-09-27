using Application.Interface;
using Application.Interface.Repositories;
using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IContentRepository ContentRepository { get; }

        public IPasswordResetTokenRepository PasswordResetTokenRepository { get; }


        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context,
            ICategoryRepository categoryRepository,
            IContentRepository contentRepository,
            IPasswordResetTokenRepository passwordResetTokenRepository)
        {
            _context = context;
            CategoryRepository = categoryRepository;
            ContentRepository = contentRepository;
            PasswordResetTokenRepository = passwordResetTokenRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
