using Dapper;
using Npgsql;
using construction.Interfaces;
using construction.Dtos;

namespace construction.Repositories;

public class JobTypesRepository
{
    private readonly string? _connectionString;

    public JobTypesRepository(IConfiguration config)
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

    public async Task<IEnumerable<GetJobTypesDto>> GetJobTypes()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<GetJobTypesDto>("SELECT * FROM job_types");
    }
}
