namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using System.Diagnostics.CodeAnalysis;
using Data.Exceptions;
using Data.Mappers;
using Domain.Abstractions;
using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Models.Game;
using External.UEX.Abstractions;
using External.UEX.Extensions;
using Local;
using Microsoft.Extensions.Logging;

internal abstract class UexGameEntityRepositoryBase<TSource, TDomain>(
    UexGameDataStateProvider stateProvider,
    UexApiDtoMapper mapper,
    ILogger logger
) : IGameEntityExternalSyncRepository<TDomain>
    where TSource : class
    where TDomain : class, IGameEntity
{
    protected ILogger Logger { get; } = logger;

    public virtual async ValueTask<GameDataState> LoadCurrentDataState(CancellationToken cancellationToken = default)
        => await stateProvider.LoadCurrentDataState(cancellationToken);

    public async ValueTask<GameEntitySyncData<TDomain>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await GetDependencies().WaitUntilReadyAsync(cancellationToken);
        try
        {
            var response = await GetInternalResponseAsync(cancellationToken).ConfigureAwait(false);
            var cacheUntil = response.CreateResponseHeaders().GetCacheUntil();
            var domainEntities = response.Result.Where(IncludeSourceModel).Select(MapToDomain).ToAsyncEnumerable();
            var dataState = await LoadCurrentDataState(cancellationToken);
            return new GameEntitySyncData<TDomain>(domainEntities, dataState, cacheUntil);
        }
        catch (ExternalApiResponseProcessingException ex)
        {
            Logger.LogError(ex, "Failed processing response from remote API");
            var entities = new List<TDomain>();
            return new GameEntitySyncData<TDomain>(entities.ToAsyncEnumerable(), MissingGameDataState.Instance, DateTimeOffset.Now + TimeSpan.FromMinutes(15));
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

    protected virtual bool IncludeSourceModel(TSource sourceModel)
        => true;

    protected virtual IDependable GetDependencies()
        => NoDependency.Instance;

    [DoesNotReturn]
    protected static ICollection<TSource> ThrowCouldNotParseResponse()
        => throw new ExternalApiResponseProcessingException($"Failed to parse response for {typeof(TSource)} from UEX API.");

    protected static UexApiResponse<ICollection<TSource>> CreateResponse(UexApiResponse? response, ICollection<TSource>? items)
        => response is not null
            ? new UexApiResponse<ICollection<TSource>>(response.StatusCode, response.Headers, items ?? ThrowCouldNotParseResponse())
            : new UexApiResponse<ICollection<TSource>>(0, new Dictionary<string, IEnumerable<string>>(), []);

    protected abstract Task<UexApiResponse<ICollection<TSource>>> GetInternalResponseAsync(CancellationToken cancellationToken);

    protected abstract double? GetSourceApiId(TSource source);

    private async Task<TSource?> GetSingleInternalAsync(IGameEntityId id, CancellationToken cancellationToken)
    {
        if (id is not UexApiGameEntityId uexApiId)
        {
            throw new NotSupportedException($"UEX API request cannot be performed based on entity ID of: {id.GetType()}");
        }

        var response = await GetInternalResponseAsync(cancellationToken).ConfigureAwait(false);
        return response.Result.FirstOrDefault(source => uexApiId.Equals(GetSourceApiId(source)));
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
