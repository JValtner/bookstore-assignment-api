using BookstoreApplication.Models;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Repository
{
    public interface IAuthorsRepository
    {
        Task<Author> AddAsync(Author author);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<List<Author>> GetAllAsync();
        public Task<PaginatedList<Author>> GetAllPagedAsync(int page, int PageSize);
        Task<Author?> GetByIdAsync(int id);
        Task<Author> UpdateAsync(Author author);
    }
}