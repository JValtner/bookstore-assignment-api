using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IPublishersService
    {
        Task<Publisher> AddAsync(Publisher publisher);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<List<Publisher>> GetAllAsync();
        Task<Publisher?> GetByIdAsync(int id);
        Task<Publisher> UpdateAsync(int id, Publisher publisher);
    }
}