using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace MapRestApi.Models.Geometry
{
    public class ObjectGeometry
    {
        [BsonElement("type")]
        public string Type { get; set; } = "Point";

        [BsonElement("coordinates")]
        public required GeoJson2DCoordinates Coordinates { get; set; }
    }
}
