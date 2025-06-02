using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Water_SF.Data;
using Water_SF.Services;

namespace Water_SF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioService _inventarioService;

        public InventarioController(IInventarioService inventarioService)
        {
            _inventarioService = inventarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _inventarioService.Get(null);
            return Ok(items);
        }

        [HttpGet("{inventarioId}")]
        public async Task<IActionResult> Get(string inventarioId)
        {
            var items = await _inventarioService.Get(new[] { inventarioId });
            var item = items.FirstOrDefault();

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Inventario item)
        {
            await _inventarioService.Add(item);
            return Ok(item);
        }

        [HttpPut("{inventarioId}")]
        public async Task<IActionResult> Update(string inventarioId, Inventario item)
        {
            if (inventarioId != item.Id)
                return BadRequest("El ID de la URL no coincide con el del inventario.");

            var existente = (await _inventarioService.Get(new[] { inventarioId })).FirstOrDefault();
            if (existente == null)
                return NotFound();

            await _inventarioService.Update(item);
            return NoContent();
        }

        [HttpDelete("{inventarioId}")]
        public async Task<IActionResult> Delete(string inventarioId)
        {
            var existente = (await _inventarioService.Get(new[] { inventarioId })).FirstOrDefault();
            if (existente == null)
                return NotFound();

            await _inventarioService.Delete(existente);
            return NoContent();
        }
    }
}

