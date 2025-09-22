using MapRestApi.Models;
using MapRestApi.Models.Common;
using MapRestApi.Repositories;
using MapRestApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MapRestApi.Controllers
{
    [ApiController]
    [Route("api/polygons")]
    public class PolygonController : ControllerBase
    {
        private readonly IPolygonRepository _polygonRepo;

        public PolygonController(IPolygonRepository polygonRepo)
        {
            _polygonRepo = polygonRepo;
        }

        /// <summary>
        /// Get all polygons.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Polygon>>> Get()
        {
            try
            {
                var polygons = await _polygonRepo.GetPolygonsAsync();
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
                var deleted = await _polygonRepo.DeletePolygonAsync(id);
                if (!deleted)
                    return NotFound($"Polygon with id '{id}' not found.");

                var polygons = await _polygonRepo.GetPolygonsAsync();
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
                await _polygonRepo.DeleteAllPolygonsAsync();
                var polygons = await _polygonRepo.GetPolygonsAsync();
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
        public async Task<ActionResult<List<Polygon>>> Save([FromBody] List<PolygonObjectDTO> polygonsDto)
        {
            if (polygonsDto == null || polygonsDto.Count == 0)
                return BadRequest("Polygon list is required.");

            try
            {
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

                await _polygonRepo.AddPolygonsBulkAsync(polygons); 
                var allPolygons = await _polygonRepo.GetPolygonsAsync();
                return Ok(allPolygons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
