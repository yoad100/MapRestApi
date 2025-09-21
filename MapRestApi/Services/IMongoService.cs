using MapRestApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MapRestApi.Services
{
    public interface IMongoService
    {
        // Polygon CRUD
        Task<List<Polygon>> GetPolygonsAsync();
        Task<bool> DeletePolygonAsync(string id);
        Task<bool> DeleteAllPolygonsAsync();

        // MapObject CRUD
        Task<List<MapObject>> GetObjectsAsync();
        Task<bool> DeleteObjectAsync(string id);
        Task AddObjectsBulkAsync(List<MapObjectDTO> objects);
        Task AddPolygonsBulkAsync(List<PolygonObjectDTO> polygons);
    }
}
