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

        GetJobDto? job = await connection.QueryFirstOrDefaultAsync<GetJobDto>("SELECT * FROM jobs WHERE job_id = @Id", new { Id = id });

        if (job == null)
        {
            return null;
        }

        var images = await connection.QueryAsync<Images>("SELECT * FROM jobs_images WHERE job_id = @Id", new { Id = id });

        job.Images = (List<Images>?)images;

        return job;
    }



    public async Task<EditJobDto?> EditJob(EditJobDto job, int id)
    {

        // create a connection
        await using var connection = new NpgsqlConnection(_connectionString);

        // build sql
        StringBuilder sql = new StringBuilder();
        sql.Append("UPDATE jobs");
        sql.Append(" SET title = @Title,");
        sql.Append(" tagline = @Tagline,");
        sql.Append(" description = @Description,");
        sql.Append(" client = @Client,");
        sql.Append(" location = @Location");
        sql.Append(" WHERE job_id = @Job_Id");
        sql.Append(" RETURNING title, tagline, description, client, location");

        // update and store job
        EditJobDto? updatedJob = await connection.QueryFirstOrDefaultAsync<EditJobDto>(sql.ToString(), new
        {
            Title = job.Title,
            Tagline = job.Tagline,
            Description = job.Description,
            Client = job.Client,
            Location = job.Location,
            Job_Id = id
        });

        // return updated job
        return updatedJob;
    }
}
