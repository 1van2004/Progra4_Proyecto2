using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Water_SF.Data
{
    public class ReporteContext : DbContext
    {
        public ReporteContext(DbContextOptions<ReporteContext> options)
            : base(options)
        { }

        public DbSet<Reporte> Reportes { get; set; }
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
}
