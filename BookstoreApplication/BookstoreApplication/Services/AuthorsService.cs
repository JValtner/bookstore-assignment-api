using BookstoreApplication.Models;
using BookstoreApplication.Repository;
using BookstoreApplication.Exceptions;
namespace BookstoreApplication.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IAuthorsRepository _authorsRepository;
        private readonly IBooksRepository _booksRepository;
        //Cirkularne zavisnosti - ne možemo AuthorsService da koristimo BooksService koji opet koristi AuthorsService
        //private readonly IBooksService _booksService;

        public AuthorsService(IAuthorsRepository authorsRepository, IBooksRepository booksRepository)
        {
            _authorsRepository = authorsRepository;
            _booksRepository = booksRepository;
            //_booksService = booksService;
        }
        public async Task<List<Author>> GetAllAsync()
        {
            return await _authorsRepository.GetAllAsync();
        }
        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _authorsRepository.GetByIdAsync(id);
        }
        public async Task<Author> AddAsync(Author author)
        {
            return await _authorsRepository.AddAsync(author);
        }
        public async Task<Author> UpdateAsync(int id, Author author)
        {
            if (id != author.Id)
            {
                throw new BadRequestException(id);
            }

            if (!await ExistsAsync(id))
            {
                throw new NotFoundException(id);
            }
            return await _authorsRepository.UpdateAsync(author);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            if (!await ExistsAsync(id))
            {
                throw new NotFoundException(id);
            }
            // kaskadno brisanje svih knjiga obrisanog autora
            if (!await _booksRepository.DeleteAllForAuthorAsync(id))
            {
                throw new CascadeDeleteException(id);
            }

            return await _authorsRepository.DeleteAsync(id);
            // TODO kaskadno brisanje svih vezanih knjiga AuthorsAwards
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _authorsRepository.ExistsAsync(id);
        }
    }
}
