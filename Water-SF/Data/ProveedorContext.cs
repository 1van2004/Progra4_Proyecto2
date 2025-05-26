using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Water_SF.Data
{
    public class ProveedorContext : DbContext
    {
        public ProveedorContext(DbContextOptions<ProveedorContext> options)
            : base(options)
        { }

        public DbSet<Proveedor> Proveedores { get; set; }
    }

    public class Proveedor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Autoincremento
        public int Id { get; set; }

        public string NombreEmpresa { get; set; }
        public string NombreRepresentante { get; set; }
        public string CedulaRepresentante { get; set; }
        public string CorreoEmpresa { get; set; }
        public string TelefonoEmpresa { get; set; }
        public string DescripcionProductos { get; set; }
        public string NumeroCuenta { get; set; }
    }
}
