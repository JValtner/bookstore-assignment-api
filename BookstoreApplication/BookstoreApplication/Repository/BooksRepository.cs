using System;
using BookstoreApplication.Models;
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
    }
}
