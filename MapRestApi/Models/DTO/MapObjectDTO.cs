using MapRestApi.Models.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace MapRestApi.Models.DTO
{
    public class MapObjectDTO
    {
        [BsonElement("type")]
        public string Type { get; set; } = "Feature";

        [BsonElement("geometry")]
        public required ObjectGeometryDTO Geometry { get; set; }

        [BsonElement("properties")]
        public required ObjectProperties Properties { get; set; }
    }
}
