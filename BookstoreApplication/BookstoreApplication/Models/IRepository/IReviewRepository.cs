using BookstoreApplication.Models;

namespace BookstoreApplication.Repositories
{
    public interface IReviewRepository
    {
        Task AddAsync(Review review);
        Task<Review> GetByIdAsync(int id);
        Task<List<Review>> GetByBookIdAsync(int bookId);
    }
}
