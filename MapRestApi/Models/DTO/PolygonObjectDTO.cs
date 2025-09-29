using MapRestApi.Models.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace MapRestApi.Models.DTO
{
    public class PolygonObjectDTO
    {
        [BsonElement("type")]
        public string Type { get; set; } = "Feature";

        [BsonElement("geometry")]
        public required PolygonGeometryDTO Geometry { get; set; }

        [BsonElement("properties")]
        public required PolygonProperties Properties { get; set; }
    }
}
