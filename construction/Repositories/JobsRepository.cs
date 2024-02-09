using System.Text;
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

    public async Task<GetJobDto?> GetJob(int id)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        StringBuilder sql = new StringBuilder();

        sql.Append("SELECT j.* ");
        sql.Append("JSON_AGG(ji) AS images ");
        sql.Append("FROM jobs j ");
        sql.Append("LEFT JOIN jobs_images ji ON j.job_id = ji.job_id ");
        sql.Append("WHERE j.job_id = @Id ");
        sql.Append("GROUP BY j.job_id ");

        return await connection.QueryFirstOrDefaultAsync<GetJobDto>(sql.ToString(), new { Id = id });
    }
}
