using Microsoft.EntityFrameworkCore;
using Water_SF.DTO;

namespace Water_SF.Data
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        { }

        public DbSet<Users> Users { get; set; }
    }
}
