namespace Arkanis.Overlay.Infrastructure.Data.Extensions;

using Entities.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public static class DbSetExtensions
{
    public static async Task<bool> ExistsAsync<TEntity>(this DbSet<TEntity> dbSet, TEntity currentEntity, CancellationToken cancellationToken = default)
        where TEntity : class, IDatabaseEntity
        => await dbSet.AnyAsync(existingEntity => existingEntity.Equals(currentEntity), cancellationToken);

    // See: https://stackoverflow.com/a/76121820/4161937
    public static async Task<EntityEntry<TEntity>> AddOrUpdateAsync<TEntity>(
        this DbSet<TEntity> dbSet,
        TEntity currentEntity,
        CancellationToken cancellationToken = default
    )
        where TEntity : class, IDatabaseEntity
    {
        var exists = await dbSet.ExistsAsync(currentEntity, cancellationToken);
        return exists
            ? dbSet.Update(currentEntity)
            : await dbSet.AddAsync(currentEntity, cancellationToken);
    }
}
