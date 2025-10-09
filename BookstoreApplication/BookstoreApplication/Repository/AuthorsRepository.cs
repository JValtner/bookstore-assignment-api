using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepository;
using BookstoreApplication.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repository
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private BookStoreDbContext _context;

        public AuthorsRepository(BookStoreDbContext context)
        {
            _context = context;
        }
        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }
        public async Task<PaginatedList<Author>> GetAllPagedAsync(int page, int PageSize)
        {
            IQueryable<Author> authors = _context.Authors;

            int pageIndex = page - 1;
            var count = await authors.CountAsync();
            var items = await authors.Skip(pageIndex * PageSize).Take(PageSize).ToListAsync();
            PaginatedList<Author> result = new PaginatedList<Author>(items, count, pageIndex, PageSize); // važno! PageSize treba dodati kao svojstvo na početku klase (private const int PageSize = 4;)
            return result;
        }
        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }
        public async Task<Author> AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }
        public async Task<Author> UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return author;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            Author author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return false;
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
