using Application.Behaviors;
using Application.Features.Authentication.Validators;
using FluentValidation;
using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServicesInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Transient);
        services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}