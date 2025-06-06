using Microsoft.EntityFrameworkCore;
using Water_SF.Data;
using DTO = Water_SF.DTO;

namespace Water_SF.Services
{
    public class ReportesService : IReportesService
    {
        private readonly WaterSFContext _context;

        public ReportesService(WaterSFContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DTO.Reporte>> Get(string[] ids)
        {
            var stringIds = ids?.Where(id => !string.IsNullOrWhiteSpace(id)).ToList();
            var query = _context.Reportes.AsQueryable();

            if (stringIds != null && stringIds.Any())
                query = query.Where(r => stringIds.Contains(r.Id));

            return await query
                .Select(r => new DTO.Reporte
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Direccion = r.Direccion,
                    Tiporeporte = r.Tiporeporte,
                    DescripcionFuga = r.DescripcionFuga,
                    UbicacionReferencia = r.UbicacionReferencia,
                    FechaHora = r.FechaHora
                })
                .ToListAsync();
        }

        public async Task<DTO.Reporte> Add(DTO.Reporte reporte)
        {
            var entity = new Data.Reporte
            {
                Id = reporte.Id,
                Nombre = reporte.Nombre,
                Direccion = reporte.Direccion,
                Tiporeporte = reporte.Tiporeporte,
                DescripcionFuga = reporte.DescripcionFuga,
                UbicacionReferencia = reporte.UbicacionReferencia,
                FechaHora = reporte.FechaHora
            };

            await _context.Reportes.AddAsync(entity);
            await _context.SaveChangesAsync();
            return reporte;
        }

        public async Task<bool> Delete(DTO.Reporte reporte)
        {
            try
            {
                var existente = await _context.Reportes.FindAsync(reporte.Id);
                if (existente == null)
                    return false;

                _context.Reportes.Remove(existente);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

    public interface IReportesService
    {
        Task<IEnumerable<DTO.Reporte>> Get(string[] ids);
        Task<DTO.Reporte> Add(DTO.Reporte reporte);
        Task<bool> Delete(DTO.Reporte reporte);
    }
}