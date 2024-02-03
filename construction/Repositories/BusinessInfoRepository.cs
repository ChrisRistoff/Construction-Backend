using Dapper;
using Npgsql;
using construction.Interfaces;
using construction.Dtos;

namespace construction.Repositories;

public class BusinessInfoRepository : IBusinessInfoRepository
{

    private readonly string? _connectionString;


    // inject configuration
    public BusinessInfoRepository(IConfiguration config)
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



    public async Task<GetBusinessInfoDto?> GetBusinessInfo()
    {

        // create a connection
        using var connection = new NpgsqlConnection(_connectionString);

        // get and return business info
        return await connection.QueryFirstOrDefaultAsync<GetBusinessInfoDto>("SELECT * FROM business_info WHERE info_id = 1");
    }



    public async Task<UpdateBusinessInfoDto?> UpdateBusinessInfo(UpdateBusinessInfoDto businessInfo)
    {

        // create a connection
        using var connection = new NpgsqlConnection(_connectionString);

        // create parameters from business info object
        DynamicParameters parameters = new(businessInfo);

        // update business info and return updated business info without the Id
        return await connection.QueryFirstOrDefaultAsync<UpdateBusinessInfoDto>(@"
            UPDATE business_info
            SET name = @Name,
                email = @Email,
                phone = @Phone,
                address = @Address,
                city = @City,
                info = @Info,
                facebook = @Facebook,
                instagram = @Instagram,
                youtube = @Youtube,
                tiktok = @Tiktok,
                linkedin = @Linkedin
            WHERE info_id = 1
            RETURNING *
        ", parameters);
    }
}
