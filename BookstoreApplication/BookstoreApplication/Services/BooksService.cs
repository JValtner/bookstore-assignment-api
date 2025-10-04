using System;
using BookstoreApplication.Models;
using BookstoreApplication.Repository;
using BookstoreApplication.Repository;
namespace BookstoreApplication.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IAuthorsService _authorsService;
        private readonly IPublishersService _publishersService;

        public BooksService(IBooksRepository booksRepository, IAuthorsService authorsService, IPublishersService publishersService)
        {
            _booksRepository = booksRepository;
            _authorsService = authorsService;
            _publishersService = publishersService;
        }
        public async Task<List<Book>> GetAllAsync()
        {
            return await _booksRepository.GetAllAsync();
        }
        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _booksRepository.GetByIdAsync(id);
        }
        public async Task<Book?> AddAsync(Book? book)
        {
            if (book == null)
                return null;

            // kreiranje knjige je moguće ako je izabran postojeći autor
            Author author = await _authorsService.GetByIdAsync(book.AuthorId);
            if (author == null)
            {
                return null;
            }

            // kreiranje knjige je moguće ako je izabran postojeći izdavač
            Publisher publisher = await _publishersService.GetByIdAsync(book.PublisherId);
            if (publisher == null)
            {
                return null;
            }

            book.AuthorId = author.Id;
            book.Author = author;
            book.PublisherId = publisher.Id;
            book.Publisher = publisher;
            return await _booksRepository.AddAsync(book);
        }
        public async Task<Book?> UpdateAsync(int id, Book book)
        {
            if (id != book.Id)
            {
                return null;
            }

            if (!await ExistsAsync(id))
            {
                return null;
            }

            // izmena knjige je moguca ako je izabran postojeći autor
            if (!await _authorsService.ExistsAsync(book.AuthorId))
            {
                return null;
            }

            // izmena knjige je moguca ako je izabran postojeći izdavač
            if (!await _publishersService.ExistsAsync(book.PublisherId))
            {
                return null;
            }

            book.Author = await _authorsService.GetByIdAsync(book.AuthorId);
            book.Publisher = await _publishersService.GetByIdAsync(book.PublisherId);

            return await _booksRepository.UpdateAsync(book);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            Book existingBook = await GetByIdAsync(id);
            if (existingBook == null)
            {
                return false;
            }
            return await _booksRepository.DeleteAsync(id);
        }
        public async Task<bool> DeleteAllForPublisherAsync(int publisherId)
        {
            return await _booksRepository.DeleteAllForPublisherAsync(publisherId);
        }
        public async Task<bool> DeleteAllForAuthorsAsync(int authorId)
        {
            return await _booksRepository.DeleteAllForAuthorAsync(authorId);
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _booksRepository.ExistsAsync(id);
        }
    }
}
