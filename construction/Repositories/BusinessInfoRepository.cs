using Dapper;
using Npgsql;
using construction.Interfaces;
using construction.Dtos;

namespace portfolio.Repositories;

public class BusinessInfoRepository
{
    private readonly string? _connectionString;

    public BusinessInfoRepository(IConfiguration config)
    {
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

    public async Task<GetBussinessInfoDto?> GetBusinessInfo()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<GetBussinessInfoDto>("SELECT * FROM business_info WHERE info_id = 1");
    }
}
