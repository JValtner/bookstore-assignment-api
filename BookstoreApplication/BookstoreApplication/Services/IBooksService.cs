using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IBooksService
    {
        Task<Book?> AddAsync(Book? book);
        Task<bool> DeleteAllForAuthorsAsync(int authorId);
        Task<bool> DeleteAllForPublisherAsync(int publisherId);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book?> UpdateAsync(int id, Book book);
    }
}