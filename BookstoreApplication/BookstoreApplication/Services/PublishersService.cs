using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using BookstoreApplication.Repository;
namespace BookstoreApplication.Services
{
    public class PublishersService : IPublishersService
    {
        private readonly IPublishersRepository _publishersRepository;
        private readonly IBooksRepository _booksRepository;
        // Cirkularne zavisnosti - ne možemo PublishersService da koristimo BooksService koji opet koristi PublishersService
        //private readonly IBooksService _booksService;
        public PublishersService(IPublishersRepository publishersRepository, IBooksRepository booksRepository)
        {
            _publishersRepository = publishersRepository;
            _booksRepository = booksRepository;
            //_booksService = booksService;
        }
        public async Task<List<Publisher>> GetAllAsync()
        {
            return await _publishersRepository.GetAllAsync();
        }
        public async Task<Publisher?> GetByIdAsync(int id)
        {
            return await _publishersRepository.GetByIdAsync(id);
        }
        public async Task<Publisher> AddAsync(Publisher publisher)
        {
            return await _publishersRepository.AddAsync(publisher);
        }
        public async Task<Publisher> UpdateAsync(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                throw new BadRequestException(id); 
            }

            Publisher existingPublisher = await GetByIdAsync(id);
            if (existingPublisher == null)
            {
                throw new NotFoundException(id);           
            }

            return await _publishersRepository.UpdateAsync(publisher);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            Publisher existingPublisher = await GetByIdAsync(id);
            if (existingPublisher == null)
            {
                throw new NotFoundException(id);
            }
            // kaskadno brisanje svih knjiga obrisanog izdavača
            await _booksRepository.DeleteAllForPublisherAsync(id);
            return await _publishersRepository.DeleteAsync(id);
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _publishersRepository.ExistsAsync(id);
        }
    }
}
