using System.Text;
using Dapper;
using Npgsql;
using construction.Interfaces;
using construction.Dtos;
using construction.Services;

namespace construction.Repositories;

public class ImageRepository
{

    private readonly string? _connectionString;
    private readonly StorageService _storageService;


    // inject configuration
    public ImageRepository(IConfiguration config, StorageService storageService)
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



    public async Task<string?> AddImage(IFormFile file)
    {
        // establish a connection
        await using var connection = new NpgsqlConnection(_connectionString);

        // upload the file to firebase storage
        string? fileLink = await _storageService.UploadFileAsync(file.OpenReadStream(), file.FileName);

        // return the file link
        return fileLink;
    }
}
