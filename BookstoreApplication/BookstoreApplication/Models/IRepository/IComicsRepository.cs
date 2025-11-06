using BookstoreApplication.Models.ExternalComics;

namespace BookstoreApplication.Models.IRepository
{
    public interface IComicsRepository
    {
        Task<LocalIssue> AddAsync(LocalIssue localIssue);
    }
}