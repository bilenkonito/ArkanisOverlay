namespace Arkanis.Overlay.Infrastructure.Services.Abstractions;

using Domain.Abstractions;

public interface IHydrationServiceFor
{
    bool Matches<T>(T entity);

    Task HydrateAsync<T>(T entity, CancellationToken cancellationToken = default);
}

public interface IHydrationServiceFor<in T> : IHydrationServiceFor, IDependable
{
    bool IHydrationServiceFor.Matches<TEntity>(TEntity entity)
        => entity is T;

    async Task IHydrationServiceFor.HydrateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
    {
        if (entity is T targetEntity)
        {
            await WaitUntilReadyAsync(cancellationToken);
            await HydrateAsync(targetEntity, cancellationToken);
        }
    }

    Task HydrateAsync(T entity, CancellationToken cancellationToken = default);
}
