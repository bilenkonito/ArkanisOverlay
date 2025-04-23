namespace Arkanis.Overlay.Infrastructure.Data.Mappers;

using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    internal static IServiceCollection AddUexApiMappers(this IServiceCollection services)
        => services.AddSingleton<UexApiDtoMapper>();
}
