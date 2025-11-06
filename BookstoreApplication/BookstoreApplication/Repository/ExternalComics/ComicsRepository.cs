using BookstoreApplication.Models;
using BookstoreApplication.Models.ExternalComics;
using BookstoreApplication.Models.IRepository;

namespace BookstoreApplication.Repository.ExternalComics
{
    public class ComicsRepository : IComicsRepository
    {
        private BookStoreDbContext _context;

        public ComicsRepository(BookStoreDbContext context)
        {
            _context = context;
        }
        public async Task<LocalIssue> AddAsync(LocalIssue localIssue)
        {
            await _context.LocalIssues.AddAsync(localIssue);
            await _context.SaveChangesAsync();
            return localIssue;
        }


    }
}
