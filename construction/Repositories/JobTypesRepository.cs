using Dapper;
using Npgsql;
using construction.Interfaces;
using construction.Dtos;

namespace construction.Repositories;



public class JobTypesRepository : IJobTypesRepository
{
    private readonly string? _connectionString;


    // inject configuration
    public JobTypesRepository(IConfiguration config)
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



    public async Task<IEnumerable<GetJobTypeDto>?> GetJobTypes()
    {

        // create a connection
        using var connection = new NpgsqlConnection(_connectionString);

        // get and return job types
        return await connection.QueryAsync<GetJobTypeDto>("SELECT * FROM job_types");
    }



    public async Task<GetJobTypeDto?> GetJobType(string name)
    {

        // create a connection
        using var connection = new NpgsqlConnection(_connectionString);

        // get and return job type
        return await connection.QueryFirstOrDefaultAsync<GetJobTypeDto>("SELECT * FROM job_types WHERE name = @Name", new { Name = name });
    }
}
