using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims; 
using System.Text;
using System.Threading.Tasks;

public class UserService : IUserService 
{
    private readonly IUserRepository userRepository;
    private readonly IConfiguration config; 

    public UserService(IUserRepository userRepository, IConfiguration config) 
    {
        this.userRepository = userRepository;
        this.config = config; 
    }

    public UserModel Authenticate(string username, string password)
    {
        var user = userRepository.GetByUsernameAndPassword(username, password);

        if (user == null) 
        {
            return null;
        }

        // authentication successful so generate jwt token
        var tokenHandler = new JwtSecurityTokenHandler(); 
        var key = Encoding.ASCII.GetBytes(config.GetSection("JwtSettings:JwtSecretKey").Value);
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
        user.Token = tokenHandler.WriteToken(token);

        // remove password before returning
        user.Password = null;

        return user;
    }

    public UserModel Register(UserModel user) 
    {
        // Check if user with the same username already exists
        var existingUser = userRepository.GetByUsername(user.Username); 
        if (existingUser != null)
        {
            throw new Exception("User with the same username already exists.");
        }

        // Check if user with the same email already exists
        existingUser = userRepository.GetByEmail(user.Email);
        if (existingUser != null)
        {
            throw new Exception("User with the same email already exists.");
        }

        // Add the new user
        userRepository.Create(user); 
        return user;  
    } 
} 