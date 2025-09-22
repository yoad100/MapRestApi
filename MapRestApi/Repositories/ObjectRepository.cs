using MapRestApi.Models;
using MapRestApi.Models.Common;
using MapRestApi.Repositories.Interfaces;
using MongoDB.Driver;

namespace MapRestApi.Repositories
{
    public class ObjectRepository : IObjectRepository
    {
        private readonly IMongoCollection<MapObject> _objects;

        public ObjectRepository(IMongoDatabase database)
        {
            _objects = database.GetCollection<MapObject>("Objects");
        }

        public async Task<List<MapObject>> GetAllObjectsAsync() =>
            await _objects.Find(_ => true).ToListAsync();

        public async Task AddObjectsBulkAsync(List<MapObject> objects)
        {
            await _objects.InsertManyAsync(objects);
        }

        public async Task<bool> DeleteObjectAsync(string id)
        {
            var result = await _objects.DeleteOneAsync(o => o.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
