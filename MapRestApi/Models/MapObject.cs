using MapRestApi.Models.Common;
using MapRestApi.Models.Geometry;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MapRestApi.Models
{
    /// <summary>
    /// Represents a map object (marker or symbol) using GeoJSON Point.
    /// </summary>
    public class MapObject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("type")]
        public string Type { get; set; } = "Feature";

        [BsonElement("geometry")]
        public required ObjectGeometry Geometry { get; set; }

        [BsonElement("properties")]
        public required ObjectProperties Properties { get; set; }
    }
}
