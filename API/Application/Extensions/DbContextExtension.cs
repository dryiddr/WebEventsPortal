using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Api;
namespace Application.Extensions;

public static class DbContextExtension
{

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = "Host=localhost;Username=postgres;Password=admin;Database=WebPortalDb"; 
        services.AddDbContext<WebPortalDbContext>(options => options
            .UseNpgsql(connectionString).EnableSensitiveDataLogging());
    }
}