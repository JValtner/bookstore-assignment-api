using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Models.ExternalComics
{
    [Owned]
    public class ComicVineImage
    {
        public string Icon_url { get; set; }
        public string Medium_url { get; set; }
        public string Super_url { get; set; }
    }
}
