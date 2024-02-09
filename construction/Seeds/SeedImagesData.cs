using System.Text;
using Dapper;
using Npgsql;
using construction.Dtos;
using construction.Seeds;

namespace construction.Seed;



public class SeedImages
{


    public static async Task Seed(string? connectionString, IConfiguration configuration)
    {

        // get images info data
        Images[] imagesData = new ImagesData().GetImagesData();

        // create a connection
        await using var connection = new NpgsqlConnection(connectionString);

        Console.WriteLine("Seeding Images...");
        Console.WriteLine("--------------------------------------------------------------");

        // delete existing images table
        await connection.ExecuteAsync("DELETE FROM jobs_images");

        // create sql string
        StringBuilder sql = new StringBuilder();
        sql.Append("INSERT INTO jobs_images (image_id, job_id, image) VALUES ");
        foreach (var image in imagesData)
        {
            sql.Append($"({image.Image_Id}, {image.Job_Id}, '{image.Image}'),");
        }

        sql.Remove(sql.Length - 1, 1);

        // seed images table
        await connection.ExecuteAsync(sql.ToString());

        Console.WriteLine("Seeding images complete!");
        Console.WriteLine("--------------------------------------------------------------");
    }
}
