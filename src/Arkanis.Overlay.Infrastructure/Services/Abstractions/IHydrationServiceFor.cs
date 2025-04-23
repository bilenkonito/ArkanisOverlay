namespace Arkanis.Overlay.Infrastructure.Services.Abstractions;

using Domain.Abstractions;

public interface IHydrationServiceFor : IDependable
{
    Task HydrateAsync<T>(T entity, CancellationToken cancellationToken = default);
}

public interface IHydrationServiceFor<in T> : IHydrationServiceFor
{
    Task HydrateAsync(T entity, CancellationToken cancellationToken = default);

    async Task IHydrationServiceFor.HydrateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
    {
        if (entity is T vehicle)
        {
            await HydrateAsync(vehicle, cancellationToken);
        }
    }
}
