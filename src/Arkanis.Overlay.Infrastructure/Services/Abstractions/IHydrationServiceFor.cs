namespace Arkanis.Overlay.Infrastructure.Services.Abstractions;

using Domain.Abstractions;

public interface IHydrationServiceFor : IDependable
{
    Task HydrateAsync<T>(T entity, CancellationToken cancellationToken = default);
}

public interface IHydrationServiceFor<in T> : IHydrationServiceFor
{
    Task HydrateAsync(T entity, CancellationToken cancellationToken = default);

    Task IHydrationServiceFor.HydrateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        => entity is T vehicle
            ? HydrateAsync(vehicle, cancellationToken)
            : Task.CompletedTask;
}
