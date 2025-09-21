using MapRestApi.Models.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MapRestApi.Models
{
    /// <summary>
    /// Represents a GeoJSON Polygon.
    /// </summary>
    public class Polygon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("type")]
        public string Type { get; set; } = "Feature";

        [BsonElement("geometry")]
        public required PolygonGeometry Geometry { get; set; }

        [BsonElement("properties")]
        public required PolygonProperties Properties { get; set; }
    }
}
