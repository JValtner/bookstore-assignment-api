using BookstoreApplication.Models;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Models.IRepository
{
    public interface IPublishersRepository
    {
        Task<Publisher> AddAsync(Publisher publisher);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<List<Publisher>> GetAllAsync();
        Task<Publisher?> GetByIdAsync(int id);
        Task<Publisher> UpdateAsync(Publisher publisher);
        Task<IEnumerable<Publisher>> GetAllSorted(int sortType);
        Task<List<SortTypeOption>> GetSortTypes();

    }
}