using System.Text;
using Dapper;
using Npgsql;
using construction.Interfaces;
using construction.Dtos;
using construction.Services;

namespace construction.Repositories;

public class JobsRepository : IJobsRepository
{

    private readonly string? _connectionString;
    private readonly StorageService _storageService;


    // inject configuration
    public JobsRepository (IConfiguration config, StorageService storageService)
    {

        _storageService = storageService;

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



    public async Task<GetJobDto?> AddJob(AddJobDto job)
    {

        // create a connection
        await using var connection = new NpgsqlConnection(_connectionString);

        // create sql string
        StringBuilder sql = new StringBuilder();
        sql.Append("INSERT INTO jobs (title, tagline, description, job_type, date, client, location)");
        sql.Append(" VALUES (");
        sql.Append("@Title, @Tagline, @Description, @Job_Type, @Date, @Client, @Location");
        sql.Append(")");
        sql.Append(" RETURNING *");

        // insert and return job
        return await connection.QueryFirstOrDefaultAsync<GetJobDto>(sql.ToString(),
            new
            {
                Title = job.Title,
                Tagline = job.Tagline,
                Description = job.Description,
                Job_Type = job.Job_Type,
                Date = job.Date,
                Client = job.Client,
                Location = job.Location
            }
        );
    }



    public async Task<GetJobImageDto?> AddImageToJob(int id, IFormFile image)
    {

        // create a connection
        await using var connection = new NpgsqlConnection(_connectionString);

        // upload image to storage
        string? imageUrl = await _storageService.UploadFileAsync(image.OpenReadStream(), image.FileName);

        // create sql string
        StringBuilder sql = new StringBuilder();
        sql.Append("INSERT INTO jobs_images (job_id, image)");
        sql.Append(" VALUES (");
        sql.Append("@Job_Id, @Image");
        sql.Append(")");
        sql.Append(" RETURNING *");

        // insert and return job
        return await connection.QueryFirstOrDefaultAsync<GetJobImageDto>(sql.ToString(),
            new
            {
                Job_Id = id,
                Image = imageUrl
            }
        );
    }

    public async Task<GetJobImageDto?> DeleteImageFromJob(int imageId, string image)
    {
        // create a connection
        await using var connection = new NpgsqlConnection(_connectionString);

        // delete image from storage
        await _storageService.DeleteFileAsync(image);

        // delete image from database
        string deleteImageSql = "DELETE FROM jobs_images WHERE image_id = @ImageId RETURNING *";

        return await connection.QueryFirstOrDefaultAsync<GetJobImageDto>(deleteImageSql, new { ImageId = imageId });
    }


    public async Task<GetJobDto?> DeleteJob(int id)
    {
        // create a connection
        await using var connection = new NpgsqlConnection(_connectionString);

        // get job
        GetJobDto? job = await GetJob(id);

        if (job == null)
        {
            return null;
        }

        // delete images from storage
        foreach (var image in job.Images!)
        {
            try
            {
                if (image.Image != null) await _storageService.DeleteFileAsync(image.Image);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // delete images from database
        string deleteImagesSql = "DELETE FROM jobs_images WHERE job_id = @Id";
        await connection.ExecuteAsync(deleteImagesSql, new { Id = id });

        // delete job
        string deleteJobSql = "DELETE FROM jobs WHERE job_id = @Id RETURNING *";

        return await connection.QueryFirstOrDefaultAsync<GetJobDto>(deleteJobSql, new { Id = id });
    }
}
