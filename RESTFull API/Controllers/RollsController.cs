using Microsoft.AspNetCore.Mvc;
using RESTFull_API.DTO;
using RESTFull_API.DTOs;
using RESTFull_API.Services.Interface;

namespace RESTFull_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class RollsController : ControllerBase
    {
        private readonly IRollService _service;

        public RollsController(IRollService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<RollDto>>> Get([FromQuery] RollQuery query, CancellationToken ct)
        {
            return Ok(await _service.GetAsync(query, ct));
        }

        [HttpPost]
        public async Task<ActionResult<RollDto>> Create([FromBody] CreateRollDto dto, CancellationToken ct)
        {
            var created = await _service.CreateAsync(dto, ct);

            return Created($"/api/rolls/{created.Id}", created);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<RollDto>> Remove(Guid id, CancellationToken ct)
        {
            try
            {
                return Ok(await _service.RemoveAsync(id, ct));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet("stats")]
        public async Task<ActionResult<RollStatsDto>> GetStats(
            [FromQuery] RollStatsQuery query,
            CancellationToken ct
        )
        {
            return Ok(await _service.GetStatsAsync(query, ct));
        }
    }
}
