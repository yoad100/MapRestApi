using MapRestApi.Models;
using MapRestApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MapRestApi.Controllers
{
    [ApiController]
    [Route("api/polygons")]
    public class PolygonController : ControllerBase
    {
        private readonly IMongoService _mongoService;

        public PolygonController(IMongoService mongoService)
        {
            _mongoService = mongoService;
        }

        /// <summary>
        /// Get all polygons.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Polygon>>> Get()
        {
            try
            {
                var polygons = await _mongoService.GetPolygonsAsync();
                return Ok(polygons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a polygon by id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Polygon>>> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Polygon id is required.");

            try
            {
                var deleted = await _mongoService.DeletePolygonAsync(id);
                if (!deleted)
                    return NotFound($"Polygon with id '{id}' not found.");

                var polygons = await _mongoService.GetPolygonsAsync();
                return Ok(polygons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete all polygons.
        /// </summary>
        [HttpDelete]
        public async Task<ActionResult<List<Polygon>>> DeleteAll()
        {
            try
            {
                await _mongoService.DeleteAllPolygonsAsync();
                var polygons = await _mongoService.GetPolygonsAsync();
                return Ok(polygons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Save multiple polygons at once.
        /// </summary>
        [HttpPost("save")]
        public async Task<ActionResult<List<Polygon>>> Save([FromBody] List<PolygonObjectDTO> polygons)
        {
            if (polygons == null || polygons.Count == 0)
                return BadRequest("Polygon list is required.");

            try
            {
                await _mongoService.AddPolygonsBulkAsync(polygons); 
                var allPolygons = await _mongoService.GetPolygonsAsync();
                return Ok(allPolygons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
