using System;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repo
{
    public class BooksRepository
    {
        private BookStoreDbContext _context;

        public BooksRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAll()
        {
            List<Book> books = new List<Book>();
            books = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .ToList();
            return books;
        }
        public Book? GetById(int id)
        {
            return _context.Books.Find(id);
        }
        public bool Exists(int id)
        {
            return _context.Books.Any(b => b.Id == id);
        }
        public Book Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }
        public Book Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
            return book;
        }
        public bool Delete(int id)
        {
            Book book = _context.Books.Find(id);
            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }
        //Delete all books on bublisher delete
        public bool DeleteAllForPublisher(int publisherId)
        {
            List<Book> booksToRemove = _context.Books
            .Where(b => b.PublisherId == publisherId)
            .ToList();

            if (!booksToRemove.Any())
                return false;

            _context.Books.RemoveRange(booksToRemove);
            _context.SaveChanges();
            return true;
        }
        //Delete all books on author delete
        public bool DeleteAllForAuthor(int authorId)
        {
            List<Book> booksToRemove = _context.Books
            .Where(b => b.AuthorId == authorId)
            .ToList();

            if (!booksToRemove.Any())
                return false;

            _context.Books.RemoveRange(booksToRemove);
            _context.SaveChanges();
            return true;
        }
    }
}
