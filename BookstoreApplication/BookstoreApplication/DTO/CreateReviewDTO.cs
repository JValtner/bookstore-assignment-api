using System.ComponentModel.DataAnnotations;

namespace BookstoreApplication.DTOs
{
    public class CreateReviewDto
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }
    }
}
