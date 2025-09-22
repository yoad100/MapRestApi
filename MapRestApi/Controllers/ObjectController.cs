using MapRestApi.Models;
using MapRestApi.Models.Common;
using MapRestApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MapRestApi.Controllers
{
    [ApiController]
    [Route("api/objects")]
    public class ObjectController : ControllerBase
    {
        private readonly IObjectRepository _objectRepo;

        public ObjectController(IObjectRepository objectRepo)
        {
            _objectRepo = objectRepo;
        }

        /// <summary>
        /// Get all objects.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<MapObject>>> Get()
        {
            try
            {
                var objects = await _objectRepo.GetAllObjectsAsync();
                return Ok(objects);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete an object by id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<MapObject>>> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Object id is required.");

            try
            {
                var deleted = await _objectRepo.DeleteObjectAsync(id);
                if (!deleted)
                    return NotFound($"Object with id '{id}' not found.");

                var objects = await _objectRepo.GetAllObjectsAsync();
                return Ok(objects);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Save multiple objects at once.
        /// </summary>
        [HttpPost("save")]
        public async Task<ActionResult<List<MapObject>>> Save([FromBody] List<MapObjectDTO> objects_dto)
        {
            if (objects_dto == null || objects_dto.Count == 0)
                return BadRequest("Object list is required.");

            try
            {
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
                await _objectRepo.AddObjectsBulkAsync(objects);
                var allObjects = await _objectRepo.GetAllObjectsAsync();
                return Ok(allObjects);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
