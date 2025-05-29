using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Water_SF.Data
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        { }

        public DbSet<Users> Users { get; set; }
    }

    public class Users
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string NIS { get; set; }
        public string NumeroMedidor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public string Zona { get; set; }
        public string Correo { get; set; }
    }
}
