using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace MapRestApi.Models.Common
{
    public class ObjectGeometryDTO
    {
        [BsonElement("type")]
        public string Type { get; set; } = "Point";

        [BsonElement("coordinates")]
        public required double[] Coordinates { get; set; }
    }
}
