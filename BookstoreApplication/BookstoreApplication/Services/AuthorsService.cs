using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreApplication.Repository;
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
        public async Task<List<Models.Author>> GetAllAsync()
        {
            return await _authorsRepository.GetAllAsync();
        }
        public async Task<Models.Author?> GetByIdAsync(int id)
        {
            return await _authorsRepository.GetByIdAsync(id);
        }
        public async Task<Models.Author> AddAsync(Models.Author author)
        {
            return await _authorsRepository.AddAsync(author);
        }
        public async Task<Models.Author> UpdateAsync(Models.Author author)
        {
            return await _authorsRepository.UpdateAsync(author);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            if (!await ExistsAsync(id))
            {
                return false;
            }
            // kaskadno brisanje svih knjiga obrisanog autora
            if (!await _booksRepository.DeleteAllForAuthorAsync(id))
            {
                return false;
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
