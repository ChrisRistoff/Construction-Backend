using Dapper;
using Npgsql;
using construction.Interfaces;
using construction.Dtos;

namespace construction.Repositories;

public class JobTypesRepository : IJobTypesRepository
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

    public async Task<IEnumerable<GetJobTypeDto>?> GetJobTypes()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<GetJobTypeDto>("SELECT * FROM job_types");
    }

    public async Task<GetJobTypeDto?> GetJobType(string name)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<GetJobTypeDto>("SELECT * FROM job_types WHERE name = @Name", new { Name = name });
    }
}
