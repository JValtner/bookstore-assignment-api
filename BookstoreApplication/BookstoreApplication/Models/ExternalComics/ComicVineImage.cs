using MongoDB.Bson.Serialization.Attributes;

namespace BookstoreApplication.Models.ExternalComics
{
    public class ComicVineImage
    {
        [BsonElement("icon_url")]
        public string Icon_url { get; set; }

        [BsonElement("medium_url")]
        public string Medium_url { get; set; }

        [BsonElement("super_url")]
        public string Super_url { get; set; }
    }
}
