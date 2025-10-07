using BookstoreApplication.DTO;
using BookstoreApplication.Models;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Services
{
    public interface IBooksService
    {
        Task<Book?> AddAsync(Book? book);
        Task<bool> DeleteAllForAuthorsAsync(int authorId);
        Task<bool> DeleteAllForPublisherAsync(int publisherId);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<List<BookDTO>> GetAllAsync();
        Task<BookDetailsDTO?> GetByIdAsync(int id);
        Task<Book?> UpdateAsync(int id, Book book);
        Task<IEnumerable<BookDTO>> GetAllSortedAsync(int sortType); // dobavlja knjige sortirane po tipu
        Task<List<SortTypeOption>> GetSortTypesAsync(); //dobavlja vrste sortiranja
    }
}