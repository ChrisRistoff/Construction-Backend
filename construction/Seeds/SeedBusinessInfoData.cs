using System.Text;
using Dapper;
using Npgsql;
using construction.Dtos;

namespace construction.Seed;



public class SeedBusinessInfo
{


    public static async Task Seed(string? connectionString, IConfiguration configuration)
    {

        // get business info data
        GetBusinessInfoDto businessData = new BusinessInfoData().GetBusinessInfoData();

        // create a connection
        await using var connection = new NpgsqlConnection(connectionString);

        Console.WriteLine("Seeding Business info...");
        Console.WriteLine("--------------------------------------------------------------");

        // delete existing business info table
        await connection.ExecuteAsync("DELETE FROM business_info");

        // create sql string
        StringBuilder sql = new StringBuilder();
        sql.Append("INSERT INTO business_info (info_id, name, email, phone, address, city, info, logo, facebook, instagram, youtube, tiktok, linkedin) VALUES (");
        sql.Append("@Id, @Name, @Email, @Phone, @Address, @City, @Info, @Logo, @Facebook, @Instagram, @Youtube, @Tiktok, @Linkedin");
        sql.Append(")");

        // seed business info table
        await connection.ExecuteAsync(sql.ToString(),
            new
            {
                Id = businessData.Info_id,
                Name = businessData.Name,
                Email = businessData.Email,
                Phone = businessData.Phone,
                Address = businessData.Address,
                City = businessData.City,
                Info = businessData.Info,
                Logo = businessData.Logo,
                Facebook = businessData.Facebook,
                Instagram = businessData.Instagram,
                Youtube = businessData.Youtube,
                Tiktok = businessData.Tiktok,
                Linkedin = businessData.Linkedin
            }
        );

        Console.WriteLine("Seeding business info complete!");
        Console.WriteLine("--------------------------------------------------------------");
    }
}
