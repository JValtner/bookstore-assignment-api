using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repo
{
    public class PublishersRepository
    {
        private BookStoreDbContext _context;

        public PublishersRepository(BookStoreDbContext context)
        {
            _context = context;
        }
        public async Task<List<Publisher>> GetAllAsync()
        {
            return await _context.Publishers.ToListAsync();
        }
        public async Task<Publisher?> GetByIdAsync(int id)
        {
            return await _context.Publishers.FindAsync(id);
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Publishers.AnyAsync(p => p.Id == id);
        }
        public async Task<Publisher> AddAsync(Publisher publisher)
        {
            await _context.Publishers.AddAsync(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }
        public async Task<Publisher> UpdateAsync(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            Publisher publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return false;
            }
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
