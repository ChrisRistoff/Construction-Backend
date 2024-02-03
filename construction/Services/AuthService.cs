using System.IdentityModel.Tokens.Jwt;
using System.Text;
using construction.Interfaces;
using construction.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace construction.Services;



public class AuthService(IConfiguration configuration) : IAuthService
{


    public string GenerateJwtToken(Admin user)
    {

        // get secret key from configuration
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? string.Empty));

        // create credentials using secret key
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // create token
        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        // serialize token and return
        return new JwtSecurityTokenHandler().WriteToken(token);
    }



    public string HashPassword(string password)
    {

        // instantiate hasher
        var hasher = new PasswordHasher<Admin>();

        // return hashed password
        return hasher.HashPassword(null!, password);
    }



    public bool CheckPassword(string password, string hashedPassword)
    {

        // instantiate hasher
        var hasher = new PasswordHasher<Admin>();

        // return password check result
        return hasher.VerifyHashedPassword(null!, hashedPassword, password) != PasswordVerificationResult.Failed;
    }
}
