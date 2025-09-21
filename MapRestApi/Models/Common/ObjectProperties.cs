using MongoDB.Bson.Serialization.Attributes;

namespace MapRestApi.Models.Common
{
    public class ObjectProperties
    {
        [BsonElement("name")]
        public required string Name { get; set; }

        [BsonElement("symbol")]
        public required string Symbol { get; set; } // e.g., "Marker", "Jeep"
    }
}
