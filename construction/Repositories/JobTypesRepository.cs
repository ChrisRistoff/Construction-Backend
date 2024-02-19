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

        // upload image to storage
        string imageUrl = await _storageService.UploadFileAsync(image.OpenReadStream(), image.FileName);

        // create sql string
        StringBuilder sql = new StringBuilder();
        sql.Append("UPDATE job_types SET image = @Image WHERE name = @Name RETURNING *");

        // update and return job type
        return await connection.QueryFirstOrDefaultAsync<GetJobTypeDto>(sql.ToString(), new { Image = imageUrl, Name = name });
    }



    public Task<GetJobTypeDto?> EditJobType(string name, EditJobTypeDto jobType)
    {
        // create a connection
        using var connection = new NpgsqlConnection(_connectionString);

        // create sql string
        string sql = "UPDATE job_types SET description = @Description, icon = @Icon WHERE name = @Name RETURNING *";

        // update and return job type
        return connection.QueryFirstOrDefaultAsync<GetJobTypeDto>(sql, new { Description = jobType.Description, Icon = jobType.Icon, Name = jobType.Name });
    }
}
