using BookstoreApplication.DTO;
using BookstoreApplication.Models;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Repository
{
    public interface IBooksRepository
    {
        Task<Book> AddAsync(Book book);
        Task<bool> DeleteAllForAuthorAsync(int authorId);
        Task<bool> DeleteAllForPublisherAsync(int publisherId);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book> UpdateAsync(Book book);
        Task<PaginatedList<Book>> GetAllFilteredAndSortedAndPaged(BookFilter filter, int sortType, int page, int PageSize);
        Task<List<SortTypeOption>> GetSortTypesAsync();
    }
}