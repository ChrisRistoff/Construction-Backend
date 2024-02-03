using construction.Models;

namespace construction.Interfaces;



interface IAuthService
{

    // generate jwt token
    string GenerateJwtToken(Admin user);

    // hash password
    string HashPassword(string password);

    // check password
    bool CheckPassword(string password, string hashedPassword);
}
