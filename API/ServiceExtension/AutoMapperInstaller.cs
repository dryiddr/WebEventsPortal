using System.Reflection;
using Application.Extensions;
using Application.Mapping;

namespace ServiceExtension;

public class AutoMapperInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(ArticleMapper).Assembly);
    }
}