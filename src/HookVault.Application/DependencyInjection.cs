using FluentValidation;
using HookVault.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace HookVault.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(DependencyInjections));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
        });

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjections));

        return services;
    }
}
