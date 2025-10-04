using BookstoreApplication.Models;

namespace BookstoreApplication.Repository
{
    public interface IAwardRepository
    {
        Award Add(Award award);
        bool Delete(int id);
        List<Award> GetAll();
        Award? GetById(int id);
        Award Update(Award award);
    }
}