using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TaskManagement.Data.Context;

namespace TaskManagement.Data;

/// <summary>
/// To avoid the use of startup class for migrations.
/// Source: https://geeksarray.com/blog/entity-framework-core-code-first-migration-using-separate-assembly
/// Put connection string to your local MSSQL database into appsettings.local.json
/// {
///   "ConnectionStrings": {
///      "DefaultConnection": "Server=localhost,1433;Initial Catalog=TaskManagementDb;Integrated Security=True;User Id=sa;Password=MyPass@word;"
///   }
/// }
/// Connection string could be passed in three prioritized ways:
///   1) connection string in appsettings.local.json
///   2) assigned in environment variable
///   3) passed as parameter in CLI dotnet:
///        - ef database update -- connection_string
///        - dotnet ef migrations add AddsTest -- connection_string
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TaskManagementDb>
{
    public TaskManagementDb CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.local.json", true)
            .Build();

        var builder = new DbContextOptionsBuilder<TaskManagementDb>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("No connection string found in appsettings.json");
        }

        builder.UseSqlServer(connectionString);
        return new TaskManagementDb(builder.Options);
    }
}