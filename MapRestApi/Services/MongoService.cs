using MapRestApi.Models;
using MapRestApi.Models.Common;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapRestApi.Services
{
    public class MongoService : IMongoService
    {
        private readonly IMongoCollection<Polygon> _polygons;
        private readonly IMongoCollection<MapObject> _objects;

        public MongoService(IConfiguration config)
        {
            var mongoUri = Environment.GetEnvironmentVariable("MONGODB_URI")
                           ?? "mongodb://localhost:27017"; // fallback for dev
            var client = new MongoClient(mongoUri);
            var database = client.GetDatabase("MapDb");
            _polygons = database.GetCollection<Polygon>("Polygons");
            _objects = database.GetCollection<MapObject>("Objects");
        }

        // Polygon CRUD
        public async Task<List<Polygon>> GetPolygonsAsync() =>
            await _polygons.Find(_ => true).ToListAsync();

        public async Task AddPolygonsBulkAsync(List<PolygonObjectDTO> polygonsDto)
        {
            if (polygonsDto == null || polygonsDto.Count == 0) return;

            var polygons = polygonsDto.Select(dto => new Polygon
            {
                Id = null, // let Mongo generate it
                Type = "Feature",
                Geometry = new PolygonGeometry
                {
                    Type = "Polygon",
                    Coordinates = dto.Geometry.Coordinates
                },
                Properties = new PolygonProperties
                {
                    Name = dto.Properties.Name,
                }
            }).ToList();

            await _polygons.InsertManyAsync(polygons);
        }

        public async Task<bool> DeletePolygonAsync(string id)
        {
            var result = await _polygons.DeleteOneAsync(p => p.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<bool> DeleteAllPolygonsAsync()
        {
            var result = await _polygons.DeleteManyAsync(_ => true);
            return result.DeletedCount > 0;
        }

        // MapObject CRUD
        public async Task<List<MapObject>> GetObjectsAsync() =>
            await _objects.Find(_ => true).ToListAsync();

        public async Task AddObjectsBulkAsync(List<MapObjectDTO> objects_dto)
        {
            if (objects_dto == null || objects_dto.Count == 0) return;
            var objects = objects_dto.Select(dto => new MapObject
            {
                Id = null, // Mongo generates
                Type = "Feature",
                Geometry = new ObjectGeometry
                {
                    Type = "Point",
                    Coordinates = new double[] { dto.Geometry.Coordinates[0], dto.Geometry.Coordinates[1] }
                },
                Properties = new ObjectProperties
                {
                    Name = dto.Properties.Name,
                    Symbol = dto.Properties.Symbol
                }
            }).ToList();
            await _objects.InsertManyAsync(objects);
        }

        public async Task<bool> DeleteObjectAsync(string id)
        {
            var result = await _objects.DeleteOneAsync(o => o.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
