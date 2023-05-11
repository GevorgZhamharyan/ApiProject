using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("[controller]")] 
public class LoginController : Controller 
{
    private readonly IConfiguration _config;
    private readonly IUserService _userService;

    public LoginController(IConfiguration config, IUserService userService)
    {
        _config = config;
        _userService = userService;
    }

    [HttpPost] 
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginModel login)
    {
        IActionResult response = Unauthorized();
        var user = _userService.Authenticate(login.Username, login.Password);

        if (user != null)
        {
            var tokenString = GenerateJWTToken(user); 
            response = Ok(new { token = tokenString });
        }

        return response;
    }

    private string GenerateJWTToken(UserModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config.GetSection("JwtSettings:JwtSecretKey").Value);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, user.Username)
            }), 
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                        SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
} 