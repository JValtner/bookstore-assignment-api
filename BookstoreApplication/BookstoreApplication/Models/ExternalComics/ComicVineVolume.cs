using MongoDB.Bson.Serialization.Attributes;

namespace BookstoreApplication.Models.ExternalComics
{
    public class ComicVineVolume
    {
        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("api_detail_url")]
        public string Api_detail_url { get; set; }
    }
}
