using System.Text;
using Dapper;
using Npgsql;
using construction.Models;
using construction.Services;

namespace construction.Seed;

public class SeedAdmin
{
    public static async Task Seed(string? connectionString, IConfiguration configuration)
    {
        Admin[] adminData = new AdminData().GetAdminData();

        await using var connection = new NpgsqlConnection(connectionString);

        Console.WriteLine("Seeding admin...");
        Console.WriteLine("--------------------------------------------------------------");

        // hash password
        foreach (Admin admin in adminData)
        {
            admin.Password = new AuthService(configuration).HashPassword(admin.Password!);
        }

        // delete existing admin table
        await connection.ExecuteAsync("DELETE FROM admin");

        // seed admin table
        foreach (Admin admin in adminData)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO admin (admin_id, name, password, role) VALUES (");
            sql.Append("@Id, @Name, @Password, @Role");
            sql.Append(")");
            await connection.ExecuteAsync(sql.ToString(),
                new
                {
                    Id = admin.Id,
                    Name = admin.Name,
                    Password = admin.Password,
                    Role = admin.Role,
                }
            );
        }

        Console.WriteLine("Seeding complete!");
        Console.WriteLine("--------------------------------------------------------------");
    }
}
