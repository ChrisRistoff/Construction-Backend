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



    public async Task<EditJobDto?> UpdateJob(EditJobDto job)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        StringBuilder sql = new StringBuilder();
        sql.Append("UPDATE jobs SET title = @Title, tagline = @Tagline, description = @Description, job_type = @Job_Type, date = @Date, client = @Client, location = @Location WHERE job_id = @Job_Id");

        EditJobDto? updatedJob = await connection.QueryFirstOrDefaultAsync<EditJobDto>(sql.ToString(), new
        {
            Title = job.Title,
            Tagline = job.Tagline,
            Description = job.Description,
            Job_Type = job.Job_Type,
            Date = job.Date,
            Client = job.Client,
            Location = job.Location,
            Job_Id = job.Job_Id
        });

        return updatedJob;
    }
}
