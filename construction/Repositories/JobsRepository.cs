using Dapper;
using Npgsql;
using construction.Interfaces;
using construction.Dtos;

namespace construction.Repositories;

public class JobsRepository : IJobsRepository
{

    private readonly string? _connectionString;


    // inject configuration
    public JobsRepository (IConfiguration config)
    {

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

    public async Task<IEnumerable<GetAllJobsDto>?> GetJobs()
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        return await connection.QueryAsync<GetAllJobsDto>("SELECT * FROM jobs");
    }
}
