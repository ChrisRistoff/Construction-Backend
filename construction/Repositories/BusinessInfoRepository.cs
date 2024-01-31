using Dapper;
using Npgsql;
using construction.Interfaces;
using construction.Dtos;

namespace construction.Repositories;

public class BusinessInfoRepository : IBusinessInfoRepository
{
    private readonly string? _connectionString;

    public BusinessInfoRepository(IConfiguration config)
    {
        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

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
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<GetBusinessInfoDto>("SELECT * FROM business_info WHERE info_id = 1");
    }

    public async Task<UpdateBusinessInfoDto?> UpdateBusinessInfo(UpdateBusinessInfoDto businessInfo)
    {
        using var connection = new NpgsqlConnection(_connectionString);

        DynamicParameters parameters = new(businessInfo);

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
