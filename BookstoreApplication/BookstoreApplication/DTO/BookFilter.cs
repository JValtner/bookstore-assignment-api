namespace BookstoreApplication.DTO
{
    public class BookFilter
    {
        public string? Title { get; set; }
        public DateTime? PublishedDateFrom { get; set; }
        public DateTime? PublishedDateTo { get; set; }
        public string? AuthorFullName { get; set; }
        public int? AuthorId { get; set; }
        public DateTime? AuthorDateOfBirthFrom { get; set; }
        public DateTime? AuthorDateOfBirthTo { get; set; }
    }
}
