using BookstoreApplication.Models.ExternalComics;

namespace BookstoreApplication.DTO.ExternalComics
{
    public class LocalIssueDTO
    {
        public int VineId { get; set; }
        public string? Name { get; set; }
        public ComicVineVolume Volume { get; set; } = new ComicVineVolume();
        public ComicVineImage Image { get; set; } = new ComicVineImage();
        public string? Deck { get; set; }
        public string? Description { get; set; }
        public int Issue_number { get; set; }
        public string? Site_detail_url { get; set; }
        public string? Date_added { get; set; }
        public string? Date_last_updated { get; set; }
        public int NumberOfPages { get; set; }
        public double Price { get; set; }
        public int AvailableCopies { get; set; }
    }
}
