using MapRestApi.Models;

namespace MapRestApi.Repositories.Interfaces
{
    public interface IPolygonRepository
    {
        Task<List<Polygon>> GetPolygonsAsync();
        Task AddPolygonsBulkAsync(List<Polygon> polygons);
        Task<bool> DeletePolygonAsync(string id);
        Task<bool> DeleteAllPolygonsAsync();
    }
}
