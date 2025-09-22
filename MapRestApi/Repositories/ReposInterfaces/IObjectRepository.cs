using MapRestApi.Models;

namespace MapRestApi.Repositories.Interfaces
{
    public interface IObjectRepository
    {
        Task<List<MapObject>> GetAllObjectsAsync();
        Task AddObjectsBulkAsync(List<MapObject> objects);
        Task<bool> DeleteObjectAsync(string id);
    }
}
