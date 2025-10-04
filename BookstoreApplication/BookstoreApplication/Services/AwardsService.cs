using BookstoreApplication.Repository;
using BookstoreApplication.Models;
namespace BookstoreApplication.Services
{
    public class AwardsService : IAwardsService
    {
        private readonly AwardRepository _awardsRepository;
        public AwardsService(AwardRepository awardsRepository)
        {
            _awardsRepository = awardsRepository;
        }
        public List<Award> GetAll()
        {
            return _awardsRepository.GetAll();
        }
        public Award? GetById(int id)
        {
            return _awardsRepository.GetById(id);
        }
        public Award Add(Award award)
        {
            return _awardsRepository.Add(award);
        }
        public Award Update(Award award)
        {
            return _awardsRepository.Update(award);
        }
        public bool Delete(int id)
        {
            return _awardsRepository.Delete(id);
        }

    }
}
