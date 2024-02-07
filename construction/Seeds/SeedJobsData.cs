using System.Text;
using construction.Dtos;
using construction.Seeds.Data;
using Dapper;
using Npgsql;

public class SeedJobs
{

    public static async Task Seed(string? connectionString, IConfiguration configuration)
    {
        // get job data
        GetAllJobsDto[] jobsData = new JobsData().GetJobsData();

        // create a connection
        await using var connection = new NpgsqlConnection(connectionString);

        Console.WriteLine("Seeding Jobs...");
        Console.WriteLine("--------------------------------------------------------------");

        // delete existing jobs table
        await connection.ExecuteAsync("DELETE FROM jobs");

        // create sql string
        StringBuilder sql = new StringBuilder();
        sql.Append("INSERT INTO jobs (job_id, title, tagline, description, job_type, date, client, location) VALUES ");
        foreach (var job in jobsData)
        {
            sql.Append($"({job.Id}, '{job.Title}', '{job.Tagline}', '{job.Description}', '{job.JobType}', '{job.Date}', '{job.Client}', '{job.Location}'),");
        }
        sql.Remove(sql.Length - 1, 1);

        // seed jobs table
        await connection.ExecuteAsync(sql.ToString());

        Console.WriteLine("Seeding jobs complete!");
        Console.WriteLine("--------------------------------------------------------------");
    }
}
