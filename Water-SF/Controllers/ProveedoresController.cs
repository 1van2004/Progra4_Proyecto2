using Microsoft.AspNetCore.Mvc;
using Water_SF.Data;
using Water_SF.Services;

namespace Water_SF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedoresService _proveedorService;

        public ProveedoresController(IProveedoresService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proveedores = await _proveedorService.Get(null);
            return Ok(proveedores);
        }

        [HttpGet("{proveedorId}")]
        public async Task<IActionResult> Get(string proveedorId)
        {
            var proveedores = await _proveedorService.Get(new[] { proveedorId });
            var proveedor = proveedores.FirstOrDefault();

            if (proveedor == null)
                return NotFound();

            return Ok(proveedor);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Proveedor p)
        {
            await _proveedorService.Add(p);
            return Ok(p);
        }

        [HttpPut("{proveedorId}")]
        public async Task<IActionResult> Update(string proveedorId, Proveedor proveedor)
        {
            if (proveedorId != proveedor.Id)
                return BadRequest("El ID de la URL no coincide con el del proveedor.");

            var existente = (await _proveedorService.Get(new[] { proveedorId })).FirstOrDefault();
            if (existente == null)
                return NotFound();

            await _proveedorService.Update(proveedor);
            return NoContent();
        }

        [HttpDelete("{proveedorId}")]
        public async Task<IActionResult> Delete(string proveedorId)
        {
            var existente = (await _proveedorService.Get(new[] { proveedorId })).FirstOrDefault();
            if (existente == null)
                return NotFound();

            await _proveedorService.Delete(existente);
            return NoContent();
        }
    }
}
