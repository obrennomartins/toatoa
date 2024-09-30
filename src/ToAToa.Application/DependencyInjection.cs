using Microsoft.Extensions.DependencyInjection;

namespace ToAToa.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection service)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        service.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
        service.AddAutoMapper(assembly);
    }
}
