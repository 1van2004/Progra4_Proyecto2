using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Water_SF.Data
{
    public class InventarioContext : DbContext
    {
        public InventarioContext(DbContextOptions<InventarioContext> options)
            : base(options)
        { }

        public DbSet<Inventario> Inventarios { get; set; }
    }

    public class Inventario
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public string Unidad { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal Precio { get; set; }
        public string Moneda { get; set; }

        public string Categoria { get; set; }
    }
}
