using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepository;
using BookstoreApplication.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repository
{
    public class PublishersRepository : IPublishersRepository
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
        public async Task<IEnumerable<Publisher>> GetAllSorted(int sortType) // dobavlja izdavače sortirane po tipu
        {
            IQueryable<Publisher> publishers = _context.Publishers;

            publishers = SortPublishers(publishers, sortType); // kod koji sortira izdavače je izdvojen u metodu ispod
            return await publishers.ToListAsync();
        }

        private static IQueryable<Publisher> SortPublishers(IQueryable<Publisher> publishers, int sortType)
        {
            return sortType switch
            {
                (int)PublisherSortType.NAME_ASCENDING => publishers.OrderBy(p => p.Name),
                (int)PublisherSortType.NAME_DESCENDING => publishers.OrderByDescending(p => p.Name),
                (int)PublisherSortType.ADDRESS_ASCENDING => publishers.OrderBy(p => p.Address),
                (int)PublisherSortType.ADDRESS_DESCENDING => publishers.OrderByDescending(p => p.Address),
                _ => publishers.OrderBy(p => p.Name),    // podrazumevano sortiranje je po nazivu rastuće
            };
        }

        public async Task<List<SortTypeOption>> GetSortTypes()  // dobavlja vrste sortiranja
        {
            List<SortTypeOption> options = new List<SortTypeOption>();
            var enumValues = Enum.GetValues(typeof(PublisherSortType));  // preuzimanje niza vrednosti za enumeraciju
            foreach (PublisherSortType sortType in enumValues)           // svaku vrednost za enumeraciju konvertuje u SortTypeOption
            {
                options.Add(new SortTypeOption(sortType));
            }
            return options;
        }

    }
}
