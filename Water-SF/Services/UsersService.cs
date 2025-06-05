using Microsoft.EntityFrameworkCore;
using Water_SF.Data;

namespace Water_SF.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserContext _context;

        public UsersService(UserContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> Get(string[] ids)
        {
            var users = _context.Users.AsQueryable();

            if (ids != null && ids.Any())
            {
                var intIds = ids.Select(id => int.TryParse(id, out var parsed) ? parsed : (int?)null)
                                .Where(x => x.HasValue)
                                .Select(x => x.Value)
                                .ToList();

                users = users.Where(x => intIds.Contains(x.Id));
            }

            return await users.ToListAsync();
        }

        public async Task<Users> Add(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Users> Update(Users user)
        {
            var existente = await _context.Users.SingleOrDefaultAsync(x => x.Id == user.Id);

            if (existente != null)
            {
                existente.Nis = user.Nis;
                existente.NumeroMedidor = user.NumeroMedidor;
                existente.Nombre = user.Nombre;
                existente.Apellido = user.Apellido;
                existente.Cedula = user.Cedula;
                existente.Telefono = user.Telefono;
                existente.Direccion = user.Direccion;
                existente.Correo = user.Correo;
                existente.Zona = user.Zona;

                _context.Users.Update(existente);
                await _context.SaveChangesAsync();
            }

            return user;
        }

        public async Task<bool> Delete(Users user)
        {
            try
            {
                var existente = await _context.Users.FindAsync(user.Id);
                if (existente == null) return false;

                _context.Users.Remove(existente);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public interface IUsersService
    {
        Task<IEnumerable<Users>> Get(string[] ids);
        Task<Users> Add(Users user);
        Task<Users> Update(Users user);
        Task<bool> Delete(Users user);
    }
}

