using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Api;
namespace Application.Extensions;

public static class DbContextExtension
{

    private static string Host = "eventdb.postgres.database.azure.com";
    private static string User = "carl";
    private static string DBname = "events_portal";
    private static string Password = "cOrA1jsg";
    private static string Port = "5432";

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        string connString =
               String.Format(
                   "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                   Host,
                   User,
                   DBname,
                   Port,
                   Password);
        //var connectionString = "Host=localhost;Username=carl;Password=cOrA1jsg;Database=WebPortalDb";
        services.AddDbContext<WebPortalDbContext>(options => options
            .UseNpgsql(connString).EnableSensitiveDataLogging());
    }
}