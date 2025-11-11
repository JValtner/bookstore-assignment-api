namespace BookstoreApplication.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ISBN { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
        public int BookAge { get; set; }
        public double AverageRating { get; set; }
    }
}
