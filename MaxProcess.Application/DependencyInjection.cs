
using System.Reflection;
using FluentValidation;
using MaxProcess.Application.Shared.Behavior;
using MaxProcess.Application.Shared.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MaxProcess.Application;

public static class DependencyInjection
{
    public static void ConfigureApplicationApp(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        services.AddSingleton<ITokenFactory, JwtTokenFactory>();
    }
}
