using MongoDB.Bson.Serialization.Attributes;

namespace MapRestApi.Models.Common
{
    public class ObjectGeometry
    {
        [BsonElement("type")]
        public string Type { get; set; } = "Point";

        [BsonElement("coordinates")]
        public required double[] Coordinates { get; set; } // [lat, lon]
    }
}
