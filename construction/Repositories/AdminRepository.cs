using Dapper;
using Npgsql;
using construction.Dtos;
using construction.Models;
using construction.Interfaces;
using construction.Services;

namespace construction.Repositories;



public class AdminRepository : IAdminRepository
{


    private readonly AuthService _authService;
    private readonly string? _connectionString;



    // inject configuration and auth service
    public AdminRepository(IConfiguration config, AuthService authService)
    {

        _authService = authService;

        // get the connection string
        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        // set the connection string based on the environment
        if (env == "Development")
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        if (env == "Testing")
        {
            _connectionString = config.GetConnectionString("TestConnection");
        }

        if (env == "Production")
        {
            _connectionString = config.GetConnectionString("ProductionConnection");
        }
    }



    public async Task<LoginResponseDto> LoginAdmin(LoginRequestDto user)
    {

        // create a connection
        await using var connection = new NpgsqlConnection(_connectionString);

        // get admin
        var admin = await GetUser(user.Name);

        // check if admin exists
        if (admin == null!)
        {
            return null!;
        }

        // check if password is correct
        if (!_authService.CheckPassword(user.Password, admin.Password))
        {
            return null!;
        }

        // generate token
        string token = _authService.GenerateJwtToken(admin);

        // return login response
        return new LoginResponseDto
        {
            Name = admin.Name,
            Role = admin.Role,
            Token = token,
        };
    }



    // not yet sure if I will use this as an endpoint so I will keep it private for now
    private async Task<Admin> GetUser(string username)
    {

        // create a connection
        await using var connection = new NpgsqlConnection(_connectionString);

        // get user sql query
        var sql = @"
            SELECT *
            FROM admin
            WHERE name = @Username
        ";

        // get user
        var result = await connection.QueryAsync<Admin>(sql, new {Username = username});

        // return user
        return result.FirstOrDefault()!;
    }
}
