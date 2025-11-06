using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Models.ExternalComics
{
    [Owned]
    public class ComicVineVolume
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Api_detail_url { get; set; }
    }
}
