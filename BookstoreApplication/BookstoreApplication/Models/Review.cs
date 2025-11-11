namespace BookstoreApplication.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string Comment { get; set; }= string.Empty;
        public int Rating { get; set; } = 0;
        public DateTime ReviewDate { get; set; }= DateTime.UtcNow;
    }
}
