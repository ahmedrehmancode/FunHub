using Application.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IContentRepository ContentRepository { get; }
        public IBookmarkRepository BookmarkRepository { get; }
        public IMerchandiseRepository MerchandiseRepository { get; }
        public IFeedbackRepository FeedbackRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
