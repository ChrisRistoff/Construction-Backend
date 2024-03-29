/*
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace construction.Data;

public class MyContextFactory(IConfiguration configuration) : IDesignTimeDbContextFactory<MyContext>
{

    public MyContext CreateDbContext(string[] args)
    {
        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

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

        // manually inserted configuration instead of building it in this class
        var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(connectionStringName));

        return new MyContext(optionsBuilder.Options);
    }
}
*/
