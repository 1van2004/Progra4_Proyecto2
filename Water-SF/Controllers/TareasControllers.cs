using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Water_SF.Data;
using Water_SF.Services;

namespace Water_SF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  
    public class TareasController : ControllerBase
    {
        private readonly ITareasService _tareaService;

        public TareasController(ITareasService tareasService)
        {
            _tareaService = tareasService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tareas = await _tareaService.Get(null);
            return Ok(tareas);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Tarea t)
        {
            await _tareaService.Add(t);
            return Ok(t);
        }

        [HttpPut("{tareaId}")]
        public async Task<IActionResult> Update(string tareaId, Tarea tarea)
        {
            if (tareaId != tarea.Id)
                return BadRequest("El ID de la URL no coincide con el de la tarea.");

            var existingTarea = (await _tareaService.Get(new[] { tareaId })).FirstOrDefault();
            if (existingTarea == null)
                return NotFound();

            await _tareaService.Update(tarea);
            return NoContent();
        }

        [HttpDelete("{tareaId}")]
        public async Task<IActionResult> Delete(string tareaId)
        {
            var existingTarea = (await _tareaService.Get(new[] { tareaId })).FirstOrDefault();
            if (existingTarea == null)
                return NotFound();

            await _tareaService.Delete(existingTarea);
            return NoContent();
        }

        [HttpGet("{tareaId}")]
        public async Task<IActionResult> Get(string tareaId)
        {
            var tareas = await _tareaService.Get(new[] { tareaId });

            if (tareas == null)
                return NotFound();

            return Ok(tareas);
        }
    }
}
