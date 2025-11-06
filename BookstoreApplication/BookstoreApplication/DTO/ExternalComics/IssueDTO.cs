using BookstoreApplication.Models.ExternalComics;
namespace BookstoreApplication.DTO.ExternalComics
{
    public class IssueDTO
    {
        public int Id { get; set; } //identifikator izdanja na eksternom API-u,
        public string Name { get; set; }
        public ComicVineVolume Volume { get; set; }
        public string Deck { get; set; }
        public string Description { get; set; }
        public string Issue_number { get; set; }
        public ComicVineImage Image { get; set; }
        public string Site_detail_url { get; set; }
        public string Date_added { get; set; }
        public string Date_last_updated { get; set; }
    }
}
