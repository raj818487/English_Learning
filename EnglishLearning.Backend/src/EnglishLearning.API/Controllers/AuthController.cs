using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EnglishLearning.API.Models;
using EnglishLearning.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EnglishLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController(IUserService userService, IConfiguration configuration) : ControllerBase
{
    public record LoginRequest(string Email, string Password);
    public record RegisterRequest(string Email, string Password, string Role);

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<object>>> Register([FromBody] RegisterRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace(req.Password))
            return BadRequest(ApiResponse<object>.Fail("Email and password are required"));

        var exists = await userService.GetByEmailAsync(req.Email);
        if (exists != null) return BadRequest(ApiResponse<object>.Fail("User already exists"));

        var user = await userService.CreateAsync(req.Email, req.Password, string.IsNullOrWhiteSpace(req.Role) ? "User" : req.Role);
        return Ok(ApiResponse<object>.Ok(new { user.Id, user.Email, user.Role }));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<object>>> Login([FromBody] LoginRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace(req.Password))
            return BadRequest(ApiResponse<object>.Fail("Email and password are required"));

        var user = await userService.ValidateCredentialsAsync(req.Email, req.Password);
        if (user == null) return Unauthorized(ApiResponse<object>.Fail("Invalid credentials"));

        var jwtKey = configuration["Jwt:Key"] ?? "super-secret-dev-key-with-32-char-min";
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(6),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(ApiResponse<object>.Ok(new { token = tokenString, role = user.Role }));
    }
}
