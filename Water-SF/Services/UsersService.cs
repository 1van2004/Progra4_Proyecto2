using Microsoft.EntityFrameworkCore;
using Water_SF.Data;
using Water_SF.DTO;

namespace Water_SF.Services
{
    public class UsersService : IUsersService
    {
        private readonly UsersContext _context;

        public UsersService(UsersContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> Get(string[] ids)
        {
            var intIds = ids?.Select(id => int.TryParse(id, out var parsed) ? parsed : (int?)null)
                            .Where(x => x != null)
                            .Select(x => x.Value)
                            .ToList();

            var users = _context.Users.AsQueryable();

            if (intIds != null && intIds.Any())
                users = users.Where(x => intIds.Contains(x.Id));

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
            var existing = await _context.Users.SingleAsync(x => x.Id == user.Id);

            existing.Nis = user.Nis;
            existing.NumeroMedidor = user.NumeroMedidor;
            existing.Nombre = user.Nombre;
            existing.Apellido = user.Apellido;
            existing.Cedula = user.Cedula;
            existing.Telefono = user.Telefono;
            existing.Direccion = user.Direccion;
            existing.Correo = user.Correo;
           /* existing.Zona = user.Zona;*/

            _context.Users.Update(existing);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> Delete(Users user)
        {
            var existing = await _context.Users.FindAsync(user.Id);
            if (existing == null)
                return false;

            _context.Users.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
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

