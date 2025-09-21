using MapRestApi.Models;
using MapRestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MapRestApi.Controllers
{
    [ApiController]
    [Route("api/objects")]
    public class ObjectController : ControllerBase
    {
        private readonly IMongoService _mongoService;

        public ObjectController(IMongoService mongoService)
        {
            _mongoService = mongoService;
        }

        /// <summary>
        /// Get all objects.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<MapObject>>> Get()
        {
            try
            {
                var objects = await _mongoService.GetObjectsAsync();
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
                var deleted = await _mongoService.DeleteObjectAsync(id);
                if (!deleted)
                    return NotFound($"Object with id '{id}' not found.");

                var objects = await _mongoService.GetObjectsAsync();
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
        public async Task<ActionResult<List<MapObject>>> Save([FromBody] List<MapObjectDTO> objects)
        {
            if (objects == null || objects.Count == 0)
                return BadRequest("Object list is required.");

            try
            {
                await _mongoService.AddObjectsBulkAsync(objects); 
                var allObjects = await _mongoService.GetObjectsAsync();
                return Ok(allObjects);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
