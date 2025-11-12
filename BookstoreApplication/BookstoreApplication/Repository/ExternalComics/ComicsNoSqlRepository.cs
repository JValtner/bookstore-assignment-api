using MongoDB.Driver;
using BookstoreApplication.Models.ExternalComics;
using BookstoreApplication.Models.IRepository;

namespace BookstoreApplication.Repository.ExternalComics
{
    public class ComicsNoSqlRepository : IComicsRepository
    {
        private readonly IMongoCollection<LocalIssue> _localIssues;

        public ComicsNoSqlRepository(IMongoDatabase database)
        {
            // "LocalIssues" = name of MongoDB collection
            _localIssues = database.GetCollection<LocalIssue>("LocalIssues");
        }

        public async Task<LocalIssue> AddAsync(LocalIssue localIssue)
        {
            await _localIssues.InsertOneAsync(localIssue);
            return localIssue;
        }

        
    }
}

