using MongoDB.Bson.Serialization.Attributes;

namespace MapRestApi.Models.Common
{
    public class PolygonProperties
    {
        [BsonElement("name")]
        public required string Name { get; set; }
    }
}
