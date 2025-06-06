using Microsoft.EntityFrameworkCore;
using Water_SF.Data;

namespace Water_SF.Services
{
    public class ProveedorService : IProveedoresService
    {
        private readonly WaterSFContext _context;

        public ProveedorService(WaterSFContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proveedor>> Get(string[] ids)
        {
            var intIds = ids?.Select(id => int.TryParse(id, out var parsed) ? parsed : (int?)null)
                            .Where(x => x != null)
                            .Select(x => x.Value)
                            .ToList();

            var proveedores = _context.Proveedores.AsQueryable();

            if (intIds != null && intIds.Any())
                proveedores = proveedores.Where(x => intIds.Contains(x.Id));

            return await proveedores.ToListAsync();
        }

        public async Task<Proveedor> Add(Proveedor proveedor)
        {
            await _context.Proveedores.AddAsync(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }

        public async Task<IEnumerable<Proveedor>> AddRange(IEnumerable<Proveedor> proveedores)
        {
            await _context.Proveedores.AddRangeAsync(proveedores);
            await _context.SaveChangesAsync();
            return proveedores;
        }

        public async Task<Proveedor> Update(Proveedor proveedor)
        {
            var proveedorExistente = await _context.Proveedores.SingleAsync(x => x.Id == proveedor.Id);

            proveedorExistente.NombreEmpresa = proveedor.NombreEmpresa;
            proveedorExistente.NombreRepresentante = proveedor.NombreRepresentante;
            proveedorExistente.CedulaRepresentante = proveedor.CedulaRepresentante;
            proveedorExistente.CorreoEmpresa = proveedor.CorreoEmpresa;
            proveedorExistente.TelefonoEmpresa = proveedor.TelefonoEmpresa;
            proveedorExistente.DescripcionProductos = proveedor.DescripcionProductos;
            proveedorExistente.NumeroCuenta = proveedor.NumeroCuenta;

            _context.Proveedores.Update(proveedorExistente);
            await _context.SaveChangesAsync();
            return proveedor;
        }

        public async Task<bool> Delete(Proveedor proveedor)
        {
            try
            {
                var existente = await _context.Proveedores.FindAsync(proveedor.Id);
                if (existente == null)
                    return false;

                _context.Proveedores.Remove(existente);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public interface IProveedoresService
    {
        Task<IEnumerable<Proveedor>> Get(string[] ids);
        Task<Proveedor> Add(Proveedor proveedor);
        Task<Proveedor> Update(Proveedor proveedor);
        Task<bool> Delete(Proveedor proveedor);
    }
}
