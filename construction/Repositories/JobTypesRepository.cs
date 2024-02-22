using System.Text;
using Dapper;
using Npgsql;
using construction.Interfaces;
using construction.Dtos;
using construction.Services;

namespace construction.Repositories;



public class JobTypesRepository : IJobTypesRepository
{
    private readonly string? _connectionString;
    private readonly StorageService _storageService;


    // inject configuration
    public JobTypesRepository(IConfiguration config, StorageService storageService)
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
        return await connection.QueryFirstOrDefaultAsync<GetJobTypeDto>("SELECT * FROM job_types WHERE name = @Name",new { Name = name });
    }



    public async Task<AddJobTypeDto?> CreateJobType(AddJobTypeDto jobType)
    {

        // create a connection
        using var connection = new NpgsqlConnection(_connectionString);

        // create sql string
        StringBuilder sql = new StringBuilder();
        sql.Append("INSERT INTO job_types (name, description, image, icon) VALUES (");
        sql.Append("@Name, @Description, @Image, @Icon");
        sql.Append(")");
        sql.Append(" RETURNING name, description, image, icon");

        // insert and return job type
        return await connection.QueryFirstOrDefaultAsync<AddJobTypeDto>(sql.ToString(),
            new
            {
                Name = jobType.Name,
                Description = jobType.Description,
                Image = jobType.Image,
                Icon = jobType.Icon
            }
        );
    }



    public async Task<GetJobTypeDto?> UpdateImageToJobType(string name, IFormFile image)
    {
        // create a connection
        using var connection = new NpgsqlConnection(_connectionString);

        // get job type
        var jobType = await GetJobType(name);

        // delete image from storage
        try
        {
            if (jobType!.Image != null) await _storageService.DeleteFileAsync(jobType.Image);
        }
        catch (Exception e)
        {
            Console.WriteLine("Image not found in storage");
        }

        // upload image to storage
        string imageUrl = await _storageService.UploadFileAsync(image.OpenReadStream(), image.FileName);

        // create sql string
        string sql = "UPDATE job_types SET image = @Image WHERE name = @Name RETURNING *";

        // update and return job type
        return await connection.QueryFirstOrDefaultAsync<GetJobTypeDto>(sql, new { Image = imageUrl, Name = name });
    }



    public async Task<GetJobTypeDto?> EditJobType(string name, EditJobTypeDto jobType)
    {
        // create a connection
        using var connection = new NpgsqlConnection(_connectionString);

        // create sql string
        string sql = "UPDATE job_types SET description = @Description, icon = @Icon WHERE name = @Name";

        // update job type without returning
        await connection.ExecuteAsync(sql, new { Description = jobType.Description, Icon = jobType.Icon, Name = name });

        // get and return job type
        return await GetJobType(name);
    }



    public async Task<GetJobTypeDto?> DeleteJobType(string name)
    {
        // create a connection
        using var connection = new NpgsqlConnection(_connectionString);

        // get all jobs with the job type
        string getJobsSql = "SELECT * FROM jobs WHERE job_type = @Name";

        // get all jobs with the job type
        IEnumerable<GetJobDto> jobs = await connection.QueryAsync<GetJobDto>(getJobsSql, new { Name = name });

        // get each job and delete the images and the job
        foreach (GetJobDto job in jobs)
        {
            // get all images of the job
            string getImagesSql = "SELECT * FROM jobs_images WHERE job_id = @Id";
            var images = await connection.QueryAsync<GetJobImageDto>(getImagesSql, new { Id = job.Job_Id});

            // delete each image
            foreach (var image in images)
            {
                try
                {
                    // delete each image from the storage
                    if (image.Image != null) await _storageService.DeleteFileAsync(image.Image);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            // delete each image from the database
            string deleteImagesSql = "DELETE FROM jobs_images WHERE job_id = @Id";

            await connection.ExecuteAsync(deleteImagesSql, new { Id = job.Job_Id});

            // delete the job
            string deleteJobSql = "DELETE FROM jobs WHERE job_id = @Id";
            await connection.ExecuteAsync(deleteJobSql, new { Id = job.Job_Id});
        }

        // create sql string
        string deleteJobTypeSql = "DELETE FROM job_types WHERE name = @Name RETURNING *";

        // delete and return job type
        return await connection.QueryFirstOrDefaultAsync<GetJobTypeDto>(deleteJobTypeSql, new { Name = name });
    }
}
