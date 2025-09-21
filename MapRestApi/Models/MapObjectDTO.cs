using MapRestApi.Models.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace MapRestApi.Models
{
    public class MapObjectDTO
    {
        [BsonElement("type")]
        public string Type { get; set; } = "Feature";

        [BsonElement("geometry")]
        public required ObjectGeometry Geometry { get; set; }

        [BsonElement("properties")]
        public required ObjectProperties Properties { get; set; }
    }
}
