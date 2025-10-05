using BookstoreApplication.Models;
using BookstoreApplication.Repository;
using BookstoreApplication.Exceptions;
using Microsoft.Extensions.Logging;

namespace BookstoreApplication.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IAuthorsRepository _authorsRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly ILogger<AuthorsService> _logger;

        //Cirkularne zavisnosti - ne možemo AuthorsService da koristimo BooksService koji opet koristi AuthorsService
        //private readonly IBooksService _booksService;

        public AuthorsService(IAuthorsRepository authorsRepository, IBooksRepository booksRepository, ILogger<AuthorsService> logger)
        {
            _authorsRepository = authorsRepository;
            _booksRepository = booksRepository;
            _logger = logger;
            //_booksService = booksService;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all authors from repository.");
            var authors = await _authorsRepository.GetAllAsync();
            _logger.LogInformation("Fetched {Count} authors.", authors.Count);
            return authors;
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching author with ID {Id}.", id);
            var author = await _authorsRepository.GetByIdAsync(id);

            if (author == null)
            {
                _logger.LogWarning("Author with ID {Id} not found.", id);
            }
            else
            {
                _logger.LogInformation("Author with ID {Id} retrieved successfully.", id);
            }

            return author;
        }

        public async Task<Author> AddAsync(Author author)
        {
            _logger.LogInformation("Adding new author: {Name}.", author.FullName);
            var addedAuthor = await _authorsRepository.AddAsync(author);
            _logger.LogInformation("Author '{Name}' (ID: {Id}) added successfully.", addedAuthor.FullName, addedAuthor.Id);
            return addedAuthor;
        }

        public async Task<Author> UpdateAsync(int id, Author author)
        {
            _logger.LogInformation("Updating author with ID {Id}.", id);

            if (id != author.Id)
            {
                _logger.LogWarning("Author ID mismatch: URL ID {UrlId} does not match author ID {AuthorId}.", id, author.Id);
                throw new BadRequestException(id);
            }

            if (!await ExistsAsync(id))
            {
                _logger.LogWarning("Author with ID {Id} not found for update.", id);
                throw new NotFoundException(id);
            }

            var updatedAuthor = await _authorsRepository.UpdateAsync(author);
            _logger.LogInformation("Author '{Name}' (ID: {Id}) updated successfully.", updatedAuthor.FullName, updatedAuthor.Id);
            return updatedAuthor;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Attempting to delete author with ID {Id}.", id);

            if (!await ExistsAsync(id))
            {
                _logger.LogWarning("Author with ID {Id} not found for deletion.", id);
                throw new NotFoundException(id);
            }

            _logger.LogInformation("Deleting all books for author ID {Id} (cascade delete).", id);
            if (!await _booksRepository.DeleteAllForAuthorAsync(id))
            {
                _logger.LogError("Cascade delete failed for author ID {Id}.", id);
                throw new CascadeDeleteException(id);
            }

            var deleted = await _authorsRepository.DeleteAsync(id);
            if (deleted)
            {
                _logger.LogInformation("Author with ID {Id} deleted successfully.", id);
            }
            else
            {
                _logger.LogWarning("Failed to delete author with ID {Id}.", id);
            }

            return deleted;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var exists = await _authorsRepository.ExistsAsync(id);
            _logger.LogDebug("Checked existence for author ID {Id}: {Exists}", id, exists);
            return exists;
        }
    }
}
