using AutoMapper;
using BookstoreApplication.DTO;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepository;
using BookstoreApplication.Services.IService;
using BookstoreApplication.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookstoreApplication.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IAuthorsService _authorsService;
        private readonly IPublishersService _publishersService;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksService> _logger;

        public BooksService(
            IBooksRepository booksRepository,
            IAuthorsService authorsService,
            IPublishersService publishersService,
            IMapper mapper,
            ILogger<BooksService> logger)
        {
            _booksRepository = booksRepository;
            _authorsService = authorsService;
            _publishersService = publishersService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<BookDTO>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all books from repository.");
            var books = await _booksRepository.GetAllAsync();

            _logger.LogInformation("Fetched {Count} books.", books.Count);
            var dtos = books.Select(_mapper.Map<BookDTO>).ToList();
            return dtos;
        }

        public async Task<BookDetailsDTO?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching book with ID {Id}.", id);
            Book book = await _booksRepository.GetByIdAsync(id);

            if (book == null)
            {
                _logger.LogWarning("Book with ID {Id} not found.", id);
                string msg = $"Book with ID {id} not found.";
                throw new NotFoundException(id, msg);
            }

            var dto = _mapper.Map<BookDetailsDTO>(book);
            _logger.LogInformation("Book with ID {Id} successfully retrieved.", id);
            return dto;
        }

        public async Task<Book?> AddAsync(Book? book)
        {
            if (book == null)
            {
                _logger.LogWarning("Attempted to add null book.");
                string msg = "Book object cannot be null.";
                throw new BadRequestException(0, msg);
            }

            _logger.LogInformation("Adding new book: {Title}.", book.Title);

            Author author = await _authorsService.GetByIdAsync(book.AuthorId);
            if (author == null)
            {
                _logger.LogWarning("Author with ID {AuthorId} not found for book '{Title}'.", book.AuthorId, book.Title);
                string msg = $"Author with ID {book.AuthorId} not found.";
                throw new NotFoundException(book.AuthorId, msg);
            }

            Publisher publisher = await _publishersService.GetByIdAsync(book.PublisherId);
            if (publisher == null)
            {
                _logger.LogWarning("Publisher with ID {PublisherId} not found for book '{Title}'.", book.PublisherId, book.Title);
                string msg = $"Publisher with ID {book.PublisherId} not found.";
                throw new NotFoundException(book.PublisherId, msg);
            }

            book.Author = author;
            book.Publisher = publisher;

            var addedBook = await _booksRepository.AddAsync(book);
            _logger.LogInformation("Book '{Title}' (ID: {Id}) successfully added.", addedBook.Title, addedBook.Id);

            return addedBook;
        }

        public async Task<Book?> UpdateAsync(int id, Book book)
        {
            _logger.LogInformation("Updating book with ID {Id}.", id);

            if (id != book.Id)
            {
                _logger.LogWarning("Book ID mismatch: URL ID {UrlId} does not match book ID {BookId}.", id, book.Id);
                string msg = $"Book ID mismatch: URL ID {id} does not match book ID {book.Id}.";
                throw new BadRequestException(id, msg);
            }

            if (!await ExistsAsync(id))
            {
                _logger.LogWarning("Book with ID {Id} not found for update.", id);
                string msg = $"Book with ID {id} not found.";
                throw new NotFoundException(id, msg);
            }

            if (!await _authorsService.ExistsAsync(book.AuthorId))
            {
                _logger.LogWarning("Author with ID {AuthorId} not found for book update.", book.AuthorId);
                string msg = $"Author with ID {book.AuthorId} not found.";
                throw new NotFoundException(book.AuthorId, msg);
            }

            if (!await _publishersService.ExistsAsync(book.PublisherId))
            {
                _logger.LogWarning("Publisher with ID {PublisherId} not found for book update.", book.PublisherId);
                string msg = $"Publisher with ID {book.PublisherId} not found.";
                throw new NotFoundException(book.PublisherId, msg);
            }

            book.Author = await _authorsService.GetByIdAsync(book.AuthorId);
            book.Publisher = await _publishersService.GetByIdAsync(book.PublisherId);

            var updatedBook = await _booksRepository.UpdateAsync(book);
            _logger.LogInformation("Book '{Title}' (ID: {Id}) successfully updated.", updatedBook.Title, updatedBook.Id);

            return updatedBook;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Attempting to delete book with ID {Id}.", id);
            Book existingBook = await _booksRepository.GetByIdAsync(id);

            if (existingBook == null)
            {
                _logger.LogWarning("Book with ID {Id} not found for deletion.", id);
                string msg = $"Book with ID {id} not found.";
                throw new NotFoundException(id, msg);
            }

            bool deleted = await _booksRepository.DeleteAsync(id);
            _logger.LogInformation(deleted
                ? "Book with ID {Id} successfully deleted."
                : "Failed to delete book with ID {Id}.", id);

            return deleted;
        }

        public async Task<bool> DeleteAllForPublisherAsync(int publisherId)
        {
            _logger.LogInformation("Deleting all books for publisher ID {PublisherId}.", publisherId);

            if (!await _publishersService.ExistsAsync(publisherId))
            {
                _logger.LogWarning("Publisher with ID {PublisherId} not found for cascade delete.", publisherId);
                string msg = $"Publisher with ID {publisherId} not found.";
                throw new NotFoundException(publisherId, msg);
            }

            bool result = await _booksRepository.DeleteAllForPublisherAsync(publisherId);
            _logger.LogInformation("Deleted all books for publisher ID {PublisherId}. Success: {Success}", publisherId, result);
            return result;
        }

        public async Task<bool> DeleteAllForAuthorsAsync(int authorId)
        {
            _logger.LogInformation("Deleting all books for author ID {AuthorId}.", authorId);

            if (!await _authorsService.ExistsAsync(authorId))
            {
                _logger.LogWarning("Author with ID {AuthorId} not found for cascade delete.", authorId);
                string msg = $"Author with ID {authorId} not found.";
                throw new NotFoundException(authorId, msg);
            }

            bool result = await _booksRepository.DeleteAllForAuthorAsync(authorId);
            _logger.LogInformation("Deleted all books for author ID {AuthorId}. Success: {Success}", authorId, result);
            return result;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            bool exists = await _booksRepository.ExistsAsync(id);
            _logger.LogDebug("Checked existence for book ID {Id}: {Exists}", id, exists);
            return exists;
        }
       public async Task<PaginatedList<BookDTO>> GetAllFilteredAndSortedAndPaged(
            BookFilter filter, int sortType, int page, int pageSize)
        {
            var booksPage = await _booksRepository.GetAllFilteredAndSortedAndPaged(filter, sortType, page, pageSize);

            var dtoItems = booksPage.Items.Select(b => new BookDTO
            {
                Id = b.Id,
                Title = b.Title,
                PublishedDate = b.PublishedDate,
                ISBN = b.ISBN,
                AuthorName = b.Author?.FullName ?? string.Empty,
                PublisherName = b.Publisher?.Name ?? string.Empty,
                AverageRating = b.AverageRating,
                BookAge = DateTime.UtcNow.Year - b.PublishedDate.Year
            }).ToList();

            return new PaginatedList<BookDTO>(dtoItems, booksPage.Count, booksPage.PageIndex, pageSize);
        }

        public async Task<List<SortTypeOption>> GetSortTypesAsync()  //dobavlja vrste sortiranja
        {
            return await _booksRepository.GetSortTypesAsync();
        }
    }
}
