using BookstoreApplication.DTO;
using BookstoreApplication.Models;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Services.IService
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
        Task<PaginatedList<BookDTO>> GetAllFilteredAndSortedAndPaged(BookFilter filter, int sortType, int page, int pageSize);
        Task<List<SortTypeOption>> GetSortTypesAsync();
    }
}