using Water_SF.DTO;
using Water_SF.Helpers;
using Water_SF.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Water_SF;
using Water_SF.Entities;

namespace HackerRank1.Controllers;

public record TokenResponse(string token);

public record UserCredential(string email, string password);
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthenticationService authenticationService;
    
    private readonly JwtSettings jwtSettings;

    public AuthController(IAuthenticationService _authenticationService, JwtSettings _jwtSettings)
    {
        authenticationService = _authenticationService;
        jwtSettings = _jwtSettings;
    }

    [HttpPost("/login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserCredential user) 
    {
        var validuser = await authenticationService.AuthenticateAsync(user.email, user.password);
        if (validuser is null)
            return Unauthorized();


        var token = TokenGenerator.GenerateToken(validuser, jwtSettings);

        return Ok(new TokenResponse(token));
    }

}
