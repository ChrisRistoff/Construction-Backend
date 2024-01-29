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

    public AdminRepository(IConfiguration config, AuthService authService)
    {
        _authService = authService;

        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

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

    public async Task<LoginResponseDto?> LoginAdmin(LoginRequestDto user)
    {
        await using var connection = new NpgsqlConnection(_connectionString);


        // get admin
        var admin = await GetUser(user.Name);

        // check if admin exists
        if (admin == null)
        {
            return null;
        }

        // check password
        if (!_authService.CheckPassword(user.Password, admin.Password))
        {
            return null;
        }

        string token = _authService.GenerateJwtToken(admin);

        // return user
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
        await using var connection = new NpgsqlConnection(_connectionString);

        var sql = @"
            SELECT *
            FROM admin
            WHERE name = @Username
        ";

        var result = await connection.QueryAsync<Admin>(sql, new {Username = username});
        return result.FirstOrDefault()!;
    }
}
