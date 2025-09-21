using MongoDB.Bson.Serialization.Attributes;

namespace MapRestApi.Models.Common
{
    public class PolygonGeometry
    {
        [BsonElement("type")]
        public string Type { get; set; } = "Polygon";

        [BsonElement("coordinates")]
        public required List<List<List<double>>> Coordinates { get; set; }
    }
}
