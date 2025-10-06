using BookstoreApplication.DTO;
using BookstoreApplication.Models;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Services
{
    public interface IAuthorsService
    {
        Task<Author> AddAsync(Author author);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<List<Author>> GetAllAsync();
        Task<PaginatedList<AuthorDTO>> GetAllPagedAsync(int page, int pageSize);
        Task<Author?> GetByIdAsync(int id);
        Task<Author> UpdateAsync(int id, Author author);
    }
}