using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Water_SF.DTO;
using Water_SF.Services;

namespace Water_SF.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _usersService.Get(null);
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var users = await _usersService.Get(new[] { userId.ToString() });
            var user = users.FirstOrDefault();

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Users user)
        {
            await _usersService.Add(user);
            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(int userId, Users user)
        {
            if (userId != user.Id)
                return BadRequest("El ID de la URL no coincide con el del usuario.");

            var existente = (await _usersService.Get(new[] { userId.ToString() })).FirstOrDefault();
            if (existente == null)
                return NotFound();

            await _usersService.Update(user);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            var existente = (await _usersService.Get(new[] { userId.ToString() })).FirstOrDefault();
            if (existente == null)
                return NotFound();

            await _usersService.Delete(existente);
            return NoContent();
        }
    }
}

