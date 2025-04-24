namespace Arkanis.Overlay.Infrastructure.Services.Abstractions;

using Domain.Abstractions;

/// <summary>
///     A generic interface for hydration services that act as they can hydrate objects of any type.
/// </summary>
public interface IHydrationServiceFor
{
    bool Matches<T>(T entity);

    Task HydrateAsync<T>(T entity, CancellationToken cancellationToken = default);
}

/// <summary>
///     An interface for hydration services that can hydrate objects of a specific type.
/// </summary>
/// <remarks>
///     Creating cyclic hydration dependencies will result in startup deadlock.
/// </remarks>
/// <typeparam name="T">Targeted object type</typeparam>
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
