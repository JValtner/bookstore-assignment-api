using BookstoreApplication.Controllers.Interface;
using BookstoreApplication.DTO;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepository;
using BookstoreApplication.Utils;
using Microsoft.Extensions.Logging;

namespace BookstoreApplication.Services
{
    public class PublishersService : IPublishersService
    {
        private readonly IPublishersRepository _publishersRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly ILogger<PublishersService> _logger;
        private readonly AutoMapper.IMapper _mapper;

        public PublishersService(
            IPublishersRepository publishersRepository,
            IBooksRepository booksRepository,
            ILogger<PublishersService> logger,
            AutoMapper.IMapper mapper)
        {
            _publishersRepository = publishersRepository;
            _booksRepository = booksRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<Publisher>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all publishers from repository.");
            var publishers = await _publishersRepository.GetAllAsync();
            _logger.LogInformation("Fetched {Count} publishers.", publishers.Count);
            return publishers;
        }

        public async Task<Publisher?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching publisher with ID {Id}.", id);
            var publisher = await _publishersRepository.GetByIdAsync(id);

            if (publisher == null)
            {
                _logger.LogWarning("Publisher with ID {Id} not found.", id);
            }
            else
            {
                _logger.LogInformation("Publisher with ID {Id} retrieved successfully.", id);
            }

            return publisher;
        }

        public async Task<Publisher> AddAsync(Publisher publisher)
        {
            _logger.LogInformation("Adding new publisher: {Name}.", publisher.Name);
            var addedPublisher = await _publishersRepository.AddAsync(publisher);
            _logger.LogInformation("Publisher '{Name}' (ID: {Id}) added successfully.", addedPublisher.Name, addedPublisher.Id);
            return addedPublisher;
        }

        public async Task<Publisher> UpdateAsync(int id, Publisher publisher)
        {
            _logger.LogInformation("Updating publisher with ID {Id}.", id);

            if (id != publisher.Id)
            {
                _logger.LogWarning("Publisher ID mismatch: URL ID {UrlId} does not match publisher ID {PublisherId}.", id, publisher.Id);
                throw new BadRequestException(id);
            }

            var existingPublisher = await GetByIdAsync(id);
            if (existingPublisher == null)
            {
                _logger.LogWarning("Publisher with ID {Id} not found for update.", id);
                throw new NotFoundException(id);
            }

            var updatedPublisher = await _publishersRepository.UpdateAsync(publisher);
            _logger.LogInformation("Publisher '{Name}' (ID: {Id}) updated successfully.", updatedPublisher.Name, updatedPublisher.Id);
            return updatedPublisher;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Attempting to delete publisher with ID {Id}.", id);

            var existingPublisher = await GetByIdAsync(id);
            if (existingPublisher == null)
            {
                _logger.LogWarning("Publisher with ID {Id} not found for deletion.", id);
                throw new NotFoundException(id);
            }

            _logger.LogInformation("Deleting all books for publisher ID {Id} (cascade delete).", id);
            await _booksRepository.DeleteAllForPublisherAsync(id);

            var deleted = await _publishersRepository.DeleteAsync(id);
            if (deleted)
            {
                _logger.LogInformation("Publisher with ID {Id} deleted successfully.", id);
            }
            else
            {
                _logger.LogWarning("Failed to delete publisher with ID {Id}.", id);
            }

            return deleted;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var exists = await _publishersRepository.ExistsAsync(id);
            _logger.LogDebug("Checked existence for publisher ID {Id}: {Exists}", id, exists);
            return exists;
        }

        public async Task<IEnumerable<PublisherDTO>> GetAllSortedAsync(int sortType) // dobavlja izdavače sortirane po tipu
        {
            IEnumerable<Publisher> publishers = await _publishersRepository.GetAllSorted(sortType);
            var dtos = publishers.Select(_mapper.Map<PublisherDTO>).ToList();
            return dtos;
        }


        public async Task<List<SortTypeOption>> GetSortTypes()  //dobavlja vrste sortiranja
        {
            return await _publishersRepository.GetSortTypes();
        }

    }
}
