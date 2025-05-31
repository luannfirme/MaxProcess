
using System.Reflection;
using FluentValidation;
using MaxProcess.Application.Shared.Behavior;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace MaxProcess.Application;

public static class DependencyInjection
{
    public static void ConfigureApplicationApp(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}
