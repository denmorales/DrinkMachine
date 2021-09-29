using System.Linq;
using DrinkMachine.API;
using DrinkMachine.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DrinkMachine.Tests
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
        /// <inheritdoc />
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing").ConfigureServices(
                services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                             typeof(DbContextOptions<DrinkContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    var sqliteConnection = new SqliteConnection("DataSource=:memory:");
                    sqliteConnection.Open();

                    // Add ApplicationDbContext using an in-memory database for testing.
                    services.AddDbContext<DrinkContext>(options =>
                    {
                        options.UseSqlite(sqliteConnection);
                    });
                });
        }
    }
}