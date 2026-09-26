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
        public IBookmarkRepository BookmarkRepository { get; }
        public IMerchandiseRepository MerchandiseRepository { get; }
        public IFeedbackRepository FeedbackRepository { get; }


        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context,
            ICategoryRepository categoryRepository,
            IContentRepository contentRepository,
            IBookmarkRepository bookmarkRepository,
            IMerchandiseRepository merchandiseRepository,
            IFeedbackRepository feedbackRepository)
        {
            _context = context;
            CategoryRepository = categoryRepository;
            ContentRepository = contentRepository;
            BookmarkRepository = bookmarkRepository;
            MerchandiseRepository = merchandiseRepository;
            FeedbackRepository = feedbackRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
