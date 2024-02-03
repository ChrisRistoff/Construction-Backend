using System.Text;
using Dapper;
using Npgsql;
using construction.Dtos;

namespace construction.Seed;



public class SeedJobTypes
{


    public static async Task Seed(string? connectionString, IConfiguration configuration)
    {

        // get job types data
        IEnumerable<GetJobTypeDto> jobTypesData = new JobTypesData().GetJobTypesData();

        // create a connection
        await using var connection = new NpgsqlConnection(connectionString);

        Console.WriteLine("Seeding Job Types...");
        Console.WriteLine("--------------------------------------------------------------");

        // delete existing business info table
        await connection.ExecuteAsync("DELETE FROM job_types");

        // loop through job types data to seed job types table
        foreach (var jobType in jobTypesData)
        {

            // create sql string
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO job_types (name, description, image, icon) VALUES (");
            sql.Append("@Name, @Description, @Image, @Icon");
            sql.Append(")");

            // seed job types table
            await connection.ExecuteAsync(sql.ToString(),
                new
                {
                    Name = jobType.Name,
                    Description = jobType.Description,
                    Image = jobType.Image,
                    Icon = jobType.Icon
                }
            );
        }


        Console.WriteLine("Seeding Job Types Complete!");
        Console.WriteLine("--------------------------------------------------------------");
    }
}
