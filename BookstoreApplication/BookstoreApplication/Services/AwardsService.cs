using BookstoreApplication.Repository;
using BookstoreApplication.Models;
using Microsoft.Extensions.Logging;

namespace BookstoreApplication.Services
{
    public class AwardsService : IAwardsService
    {
        private readonly AwardRepository _awardsRepository;
        private readonly ILogger<AwardsService> _logger;

        public AwardsService(AwardRepository awardsRepository, ILogger<AwardsService> logger)
        {
            _awardsRepository = awardsRepository;
            _logger = logger;
        }

        public List<Award> GetAll()
        {
            _logger.LogInformation("Fetching all awards from repository.");
            var awards = _awardsRepository.GetAll();
            _logger.LogInformation("Fetched {Count} awards.", awards.Count);
            return awards;
        }

        public Award? GetById(int id)
        {
            _logger.LogInformation("Fetching award with ID {Id}.", id);
            var award = _awardsRepository.GetById(id);

            if (award == null)
            {
                _logger.LogWarning("Award with ID {Id} not found.", id);
            }
            else
            {
                _logger.LogInformation("Award with ID {Id} retrieved successfully.", id);
            }

            return award;
        }

        public Award Add(Award award)
        {
            _logger.LogInformation("Adding new award: {Name}.", award.Name);
            var addedAward = _awardsRepository.Add(award);
            _logger.LogInformation("Award '{Name}' (ID: {Id}) added successfully.", addedAward.Name, addedAward.Id);
            return addedAward;
        }

        public Award Update(Award award)
        {
            _logger.LogInformation("Updating award with ID {Id}.", award.Id);
            var updatedAward = _awardsRepository.Update(award);
            _logger.LogInformation("Award '{Name}' (ID: {Id}) updated successfully.", updatedAward.Name, updatedAward.Id);
            return updatedAward;
        }

        public bool Delete(int id)
        {
            _logger.LogInformation("Attempting to delete award with ID {Id}.", id);
            var deleted = _awardsRepository.Delete(id);

            if (deleted)
            {
                _logger.LogInformation("Award with ID {Id} deleted successfully.", id);
            }
            else
            {
                _logger.LogWarning("Failed to delete award with ID {Id}.", id);
            }

            return deleted;
        }
    }
}
