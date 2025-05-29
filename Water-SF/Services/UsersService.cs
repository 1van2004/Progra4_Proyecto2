using Microsoft.EntityFrameworkCore;
using Water_SF.Data;

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
            var usuarios = _context.Users.AsQueryable();

            if (ids != null && ids.Any())
                usuarios = usuarios.Where(x => ids.Contains(x.Id));

            return await usuarios.ToListAsync();
        }

        public async Task<Users> Add(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<Users>> AddRange(IEnumerable<Users> users)
        {
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
            return users;
        }

        public async Task<Users> Update(Users user)
        {
            var existente = await _context.Users.SingleAsync(x => x.Id == user.Id);

            existente.NIS = user.NIS;
            existente.NumeroMedidor = user.NumeroMedidor;
            existente.Nombre = user.Nombre;
            existente.Apellido = user.Apellido;
            existente.Cedula = user.Cedula;
            existente.Telefono = user.Telefono;
            existente.Zona = user.Zona;
            existente.Correo = user.Correo;

            _context.Users.Update(existente);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> Delete(Users user)
        {
            try
            {
                var existente = await _context.Users.FindAsync(user.Id);
                if (existente == null)
                    return false;

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
        Task<IEnumerable<Users>> AddRange(IEnumerable<Users> users);
        Task<Users> Update(Users user);
        Task<bool> Delete(Users user);
    }
}
