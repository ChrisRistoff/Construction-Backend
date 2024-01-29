using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace construction.Data;

public class MyContextFactory : IDesignTimeDbContextFactory<MyContext>
{
    public MyContext CreateDbContext(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        string connectionStringName = "";
        if (env == "Testing")
        {
            connectionStringName = "TestConnection";
        }

        if (env == "Development")
        {
            connectionStringName = "DefaultConnection";
        }

        if (env == "Production")
        {
            connectionStringName = "ProductionConnection";
        }

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(connectionStringName));

        return new MyContext(optionsBuilder.Options);
    }
}
