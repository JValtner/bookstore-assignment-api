using BookstoreApplication.Models;

namespace BookstoreApplication.Services.IService
{
    public interface IAwardsService
    {
        Award Add(Award award);
        bool Delete(int id);
        List<Award> GetAll();
        Award? GetById(int id);
        Award Update(Award award);
    }
}