using Microsoft.AspNetCore.Mvc;
using Water_SF.Data;
using Water_SF.Services;

namespace Water_SF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _usersService.Get(null);
            return Ok(users);
        }

        // GET: api/Users/{id}
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var users = await _usersService.Get(new[] { userId });
            var user = users.FirstOrDefault();

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Users user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _usersService.Add(user);
            return Ok(user);
        }

        // PUT: api/Users/{id}
        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(string userId, [FromBody] Users user)
        {
            if (userId != user.Id)
                return BadRequest("El ID de la URL no coincide con el del usuario.");

            var existente = (await _usersService.Get(new[] { userId })).FirstOrDefault();
            if (existente == null)
                return NotFound();

            await _usersService.Update(user);
            return NoContent();
        }

        // DELETE: api/Users/{id}
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            var existente = (await _usersService.Get(new[] { userId })).FirstOrDefault();
            if (existente == null)
                return NotFound();

            await _usersService.Delete(existente);
            return NoContent();
        }
    }
}
