using BookstoreApplication.DTO.ExternalComics;
namespace BookstoreApplication.DTO.ExternalComics
{
    public class IssueDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ComicVineVolume Volume { get; set; }
        public string Deck { get; set; }
        public ComicVineImage Image { get; set; }
        public string Site_detail_url { get; set; }
        public string Date_added { get; set; }
        public string Date_last_updated { get; set; }
    }
}
