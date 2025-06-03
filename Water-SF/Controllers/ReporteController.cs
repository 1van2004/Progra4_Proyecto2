using Microsoft.AspNetCore.Mvc;
using Water_SF.Data;
using Water_SF.Services;
using DTO = Water_SF.DTO;

namespace Water_SF.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly IReportesService _reporteService;

    public ReportesController(IReportesService reporteService)
    {
        _reporteService = reporteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reportes = await _reporteService.Get(null);
        return Ok(reportes);
    }

    [HttpGet("{reporteId}")]
    public async Task<IActionResult> Get(string reporteId)
    {
        var reportes = await _reporteService.Get(new[] { reporteId });
        var reporte = reportes.FirstOrDefault();

        if (reporte == null)
            return NotFound();

        return Ok(reporte);
    }

    [HttpPost]
    public async Task<IActionResult> Add(DTO.Reporte r)
    {
        var result = await _reporteService.Add(r);
        return Ok(result);
    }

    [HttpDelete("{reporteId}")]
    public async Task<IActionResult> Delete(string reporteId)
    {
        var existente = (await _reporteService.Get(new[] { reporteId })).FirstOrDefault();
        if (existente == null)
            return NotFound();

        await _reporteService.Delete(existente);
        return NoContent();
    }
}
