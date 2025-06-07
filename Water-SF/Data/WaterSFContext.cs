using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Water_SF.Data
{
    public class WaterSFContext : DbContext
    {
        public WaterSFContext(DbContextOptions<WaterSFContext> options)
            : base(options)
        { }

        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Reporte> Reportes { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Users> Users { get; set; }
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

    public class Reporte
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Tiporeporte { get; set; }
        public string DescripcionFuga { get; set; }
        public string UbicacionReferencia { get; set; }
        public DateTime FechaHora { get; set; }
    }

    public class Tarea
    {
        [Key]
        public string Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string PerInCharge { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
    }

    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Nis { get; set; }
        public string NumeroMedidor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Zona { get; set; }
    }
}
