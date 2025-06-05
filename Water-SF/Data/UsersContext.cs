using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Water_SF.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
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
