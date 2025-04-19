namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Data.Exceptions;
using Data.Mappers;
using Domain.Abstractions;
using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Models.Game;

internal abstract class UexGameEntityRepositoryBase<TSource, TDomain>(UexApiDtoMapper mapper) : IGameEntityExternalSyncRepository<TDomain>
    where TSource : class
    where TDomain : class, IGameEntity
{
    public async IAsyncEnumerable<TDomain> GetAllAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await GetDependencies().WaitUntilReadyAsync(cancellationToken);
        var items = await GetAllInternalAsync(cancellationToken).ConfigureAwait(false);
        foreach (var item in items)
        {
            yield return MapToDomain(item);
        }
    }

    public async Task<TDomain?> GetAsync(IGameEntityId id, CancellationToken cancellationToken = default)
    {
        await GetDependencies().WaitUntilReadyAsync(cancellationToken);
        var result = await GetSingleInternalAsync(id, cancellationToken).ConfigureAwait(false);
        return result is not null
            ? MapToDomain(result)
            : null;
    }

    protected virtual IDependable GetDependencies()
        => NoDependency.Instance;

    [DoesNotReturn]
    protected static ICollection<TSource> ThrowCouldNotParseResponse()
        => throw new ExternalApiResponseProcessingException($"Failed to parse response for {typeof(TSource)} from UEX API.");

    protected abstract Task<ICollection<TSource>> GetAllInternalAsync(CancellationToken cancellationToken);

    protected abstract double? GetSourceApiId(TSource source);

    private async Task<TSource?> GetSingleInternalAsync(IGameEntityId id, CancellationToken cancellationToken)
    {
        if (id is not UexApiGameEntityId uexApiId)
        {
            throw new NotSupportedException($"UEX API request cannot be performed based on entity ID of: {id.GetType()}");
        }

        var entities = await GetAllInternalAsync(cancellationToken).ConfigureAwait(false);
        return entities.FirstOrDefault(source => uexApiId.Equals(GetSourceApiId(source)));
    }

    private TDomain MapToDomain(TSource source)
    {
        var domainEntity = mapper.ToGameEntity(source);
        if (domainEntity is not TDomain resultEntity)
        {
            throw new ObjectMappingException($"Expected {typeof(TSource)} to map to {typeof(TDomain)}, got {domainEntity.GetType()} instead.", null);
        }

        return resultEntity;
    }
}
