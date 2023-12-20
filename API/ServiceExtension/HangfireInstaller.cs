using Hangfire;
using Hangfire.MemoryStorage;
using Persistence.Context;

namespace ServiceExtension;

public class HangfireInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config.UseMemoryStorage());
    }
}