using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepository;
using BookstoreApplication.Repositories;
using BookstoreApplication.UoW;
using Microsoft.AspNetCore.Identity;

namespace BookstoreApplication.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IBooksRepository _bookRepo;
        private readonly IUnitOfWork _uow;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewService(IReviewRepository reviewRepo, IBooksRepository bookRepo, IUnitOfWork uow, UserManager<ApplicationUser> userManager)
        {
            _reviewRepo = reviewRepo;
            _bookRepo = bookRepo;
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ReviewDto> GetByIdAsync(int id)
        {
            var review = await _reviewRepo.GetByIdAsync(id);
            if (review == null) return null;

            return new ReviewDto
            {
                Id = review.Id,
                UserName = review.User?.UserName,
                Comment = review.Comment,
                Rating = review.Rating,
                ReviewDate = review.ReviewDate
            };
        }

        public async Task<ReviewDto> AddAsync(CreateReviewDto dto, string userId)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    throw new Exception("User not found.");

                var book = await _bookRepo.GetByIdAsync(dto.BookId);
                if (book == null)
                    throw new Exception("Book not found.");

                var review = new Review
                {
                    UserId = userId,
                    BookId = dto.BookId,
                    Rating = dto.Rating,
                    Comment = dto.Comment,
                    ReviewDate = DateTime.UtcNow
                };

                await _reviewRepo.AddAsync(review);
                await _uow.SaveChangesAsync(); // first save review

                // recalculate average rating
                var reviews = await _reviewRepo.GetByBookIdAsync(dto.BookId);
                book.AverageRating = reviews.Average(r => r.Rating);

                await _bookRepo.UpdateAsync(book); // await this properly
                await _uow.CommitAsync();          // then commit transaction

                return new ReviewDto
                {
                    Id = review.Id,
                    UserName = user.UserName,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    ReviewDate = review.ReviewDate
                };
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }

    }
}
