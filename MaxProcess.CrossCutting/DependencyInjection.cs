using MaxProcess.Application.Interfaces;
using MaxProcess.CrossCutting.Security;
using Microsoft.Extensions.DependencyInjection;

namespace MaxProcess.CrossCutting;

public static class DependencyInjection
{
    public static void ConfigureCrossCuttingApp(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
    }
}

