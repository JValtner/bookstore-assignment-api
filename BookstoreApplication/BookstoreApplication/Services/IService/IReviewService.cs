using BookstoreApplication.DTOs;

namespace BookstoreApplication.Services
{
    public interface IReviewService
    {
        Task<ReviewDto> GetByIdAsync(int id);
        Task<ReviewDto> AddAsync(CreateReviewDto dto, string userId);
    }
}
