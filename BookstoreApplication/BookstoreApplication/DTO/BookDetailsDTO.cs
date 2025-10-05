namespace BookstoreApplication.DTO
{
    public class BookDetailsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ISBN { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
    }
}
