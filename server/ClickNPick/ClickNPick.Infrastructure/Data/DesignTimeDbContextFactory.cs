using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ClickNPick.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ClickNPickDbContext>
{
    public ClickNPickDbContext CreateDbContext(string[] args)
    {
        string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
           .Build();

        var builder = new DbContextOptionsBuilder<ClickNPickDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        builder.UseSqlServer(connectionString);

        return new ClickNPickDbContext(builder.Options);
    }
}
