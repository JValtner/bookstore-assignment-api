using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BookstoreApplication.Models.ExternalComics
{
    public class LocalIssue
    {
        [BsonId] // MongoDB primary key
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("vineId")]
        public int VineId { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("volume")]
        public ComicVineVolume Volume { get; set; } = new ComicVineVolume();

        [BsonElement("image")]
        public ComicVineImage Image { get; set; } = new ComicVineImage();

        [BsonElement("deck")]
        public string? Deck { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("issue_number")]
        public int Issue_number { get; set; }

        [BsonElement("site_detail_url")]
        public string? Site_detail_url { get; set; }

        [BsonElement("date_added")]
        public string? Date_added { get; set; }

        [BsonElement("date_last_updated")]
        public string? Date_last_updated { get; set; }

        [BsonElement("number_of_pages")]
        public int NumberOfPages { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }

        [BsonElement("available_copies")]
        public int AvailableCopies { get; set; }

        [BsonElement("date_local_added")]
        public DateTime Date_local_added { get; set; } = DateTime.UtcNow;
    }
}
