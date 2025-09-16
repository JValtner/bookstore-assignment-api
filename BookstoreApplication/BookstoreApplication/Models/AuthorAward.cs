namespace BookstoreApplication.Models
{
    public class AuthorAward
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        public int AwardId { get; set; }
        public Award? Award { get; set; }
        public DateTime AwardedDate { get; set; }
    }
}
