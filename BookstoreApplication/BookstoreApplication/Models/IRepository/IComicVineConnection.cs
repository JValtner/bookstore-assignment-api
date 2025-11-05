namespace BookstoreApplication.Models.IRepository
{
    public interface IComicVineConnection
    {
        Task<string> Get(string url);
    }
}