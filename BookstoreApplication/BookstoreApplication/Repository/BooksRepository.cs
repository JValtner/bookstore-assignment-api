using System;
using BookstoreApplication.DTO;
using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepository;
using BookstoreApplication.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repository
{
    public class BooksRepository : IBooksRepository
    {
        private BookStoreDbContext _context;

        public BooksRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            List<Book> books = new List<Book>();
            books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .ToListAsync();
            return books;
        }
        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Books.AnyAsync(b => b.Id == id);
        }
        public async Task<Book> AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<Book> UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            Book book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
        //DeleteAsync all books on bublisher delete
        public async Task<bool> DeleteAllForPublisherAsync(int publisherId)
        {
            List<Book> booksToRemove = await _context.Books
            .Where(b => b.PublisherId == publisherId)
            .ToListAsync();

            if (!booksToRemove.Any())
                return false;

            _context.Books.RemoveRange(booksToRemove);
            await _context.SaveChangesAsync();
            return true;
        }
        //DeleteAsync all books on author delete
        public async Task<bool> DeleteAllForAuthorAsync(int authorId)
        {
            List<Book> booksToRemove = await _context.Books
            .Where(b => b.AuthorId == authorId)
            .ToListAsync();

            if (!booksToRemove.Any())
                return false;

            _context.Books.RemoveRange(booksToRemove);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<PaginatedList<Book>> GetAllFilteredAndSortedAndPaged(BookFilter filter, int sortType, int page, int PageSize)
        {
            IQueryable<Book> books = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher);

            books = FilterBooks(books, filter);
            books = SortBooks(books, sortType);

            int pageIndex = page - 1;
            var count = await books.CountAsync();
            var items = await books.Skip(pageIndex * PageSize).Take(PageSize).ToListAsync();
            PaginatedList<Book> result = new PaginatedList<Book>(items, count, pageIndex, PageSize);
            return result;
        }
        private static IQueryable<Book> SortBooks(IQueryable<Book> books, int sortType)
        {
            return sortType switch
            {
                (int)BookSortType.BOOK_NAME_ASCENDING => books.OrderBy(b => b.Title),
                (int)BookSortType.BOOK_NAME_DESCENDING => books.OrderByDescending(b => b.Title),
                (int)BookSortType.PUBLISH_DATE_ASCENDING => books.OrderBy(b => b.PublishedDate),
                (int)BookSortType.PUBLISH_DATE_DESCENDING => books.OrderByDescending(b => b.PublishedDate),
                (int)BookSortType.AUTHOR_NAME_ASCENDING => books.OrderBy(b => b.Author.FullName),
                (int)BookSortType.AUTHOR_NAME_DESCENDING => books.OrderByDescending(b => b.Author.FullName),
                _ => books.OrderBy(b => b.Title),    // podrazumevano sortiranje je po nazivu rastuće
            };
        }
        private static IQueryable<Book> FilterBooks(IQueryable<Book> books, BookFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Title))
            {
                books = books.Where(b => b.Title.ToLower().Contains(filter.Title.ToLower()));
            }
            if (filter.PublishedDateFrom != null)
            {
                books = books.Where(b => b.PublishedDate >= filter.PublishedDateFrom);
            }

            if (filter.PublishedDateTo != null)
            {
                books = books.Where(b => b.PublishedDate <= filter.PublishedDateTo);
            }

            if (!string.IsNullOrEmpty(filter.AuthorFullName))
            {
                books = books.Where(b => b.Author.FullName.ToLower().Contains(filter.AuthorFullName.ToLower()));
            }

            if (filter.AuthorId != null)
            {
                books = books.Where(b => b.AuthorId == filter.AuthorId);
            }
            if (filter.AuthorDateOfBirthFrom != null)
            {
                books = books.Where(b => b.Author.DateOfBirth >= filter.AuthorDateOfBirthFrom);
            }

            if (filter.AuthorDateOfBirthTo != null)
            {
                books = books.Where(b => b.Author.DateOfBirth <= filter.AuthorDateOfBirthTo);
            }

            return books;
        }

        public async Task<List<SortTypeOption>> GetSortTypesAsync() 
        {
            List<SortTypeOption> options = new List<SortTypeOption>();
            var enumValues = Enum.GetValues(typeof(BookSortType));  // preuzimanje niza vrednosti za enumeraciju
            foreach (BookSortType sortType in enumValues)           // svaku vrednost za enumeraciju konvertuje u SortTypeOption
            {
                options.Add(new SortTypeOption(sortType));
            }
            return options;
        }
    }
}
