using MapRestApi.Models;
using MapRestApi.Models.Common;
using MapRestApi.Repositories.Interfaces;
using MongoDB.Driver;
using static MapRestApi.Repositories.Interfaces.IPolygonRepository;

namespace MapRestApi.Repositories
{
    public class PolygonRepository : IPolygonRepository
    {
        private readonly IMongoCollection<Polygon> _polygons;

        public PolygonRepository(IMongoDatabase database)
        {
            _polygons = database.GetCollection<Polygon>("Polygons");
        }

        public async Task<List<Polygon>> GetPolygonsAsync() =>
            await _polygons.Find(_ => true).ToListAsync();

        public async Task AddPolygonsBulkAsync(List<Polygon> polygons)
        {
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
    }
}
