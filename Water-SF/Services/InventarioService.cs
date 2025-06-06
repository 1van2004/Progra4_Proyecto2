using Microsoft.EntityFrameworkCore;
using Water_SF.Data;

namespace Water_SF.Services
{
    public class InventarioService : IInventarioService
    {
        private readonly WaterSFContext _context;

        public InventarioService(WaterSFContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventario>> Get(string[] ids)
        {
            var inventario = _context.Inventarios.AsQueryable();

            if (ids != null && ids.Any())
                inventario = inventario.Where(x => ids.Contains(x.Id));

            return await inventario.ToListAsync();
        }

        public async Task<Inventario> Add(Inventario item)
        {
            await _context.Inventarios.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<Inventario>> AddRange(IEnumerable<Inventario> items)
        {
            await _context.Inventarios.AddRangeAsync(items);
            await _context.SaveChangesAsync();
            return items;
        }

        public async Task<Inventario> Update(Inventario item)
        {
            var existente = await _context.Inventarios.SingleAsync(x => x.Id == item.Id);

            existente.Nombre = item.Nombre;
            existente.Descripcion = item.Descripcion;
            existente.Cantidad = item.Cantidad;
            existente.Unidad = item.Unidad;
            existente.FechaIngreso = item.FechaIngreso;
            existente.Precio = item.Precio;
            existente.Moneda = item.Moneda;
            existente.Categoria = item.Categoria;

            _context.Inventarios.Update(existente);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> Delete(Inventario item)
        {
            try
            {
                var existente = await _context.Inventarios.FindAsync(item.Id);
                if (existente == null)
                    return false;

                _context.Inventarios.Remove(existente);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public interface IInventarioService
    {
        Task<IEnumerable<Inventario>> Get(string[] ids);
        Task<Inventario> Add(Inventario item);
        Task<Inventario> Update(Inventario item);
        Task<bool> Delete(Inventario item);
    }
}
