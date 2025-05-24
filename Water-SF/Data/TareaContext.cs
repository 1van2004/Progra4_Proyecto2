using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Water_SF.Data
{
    public class TareaContext : DbContext
    {
        public TareaContext(DbContextOptions<TareaContext> options)
            : base(options)
        { }

        public DbSet<Tarea> Tareas { get; set; }
    }

    public class Tarea
    {
        [Key]
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PerInCharge { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
    }

    
}

