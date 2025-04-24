namespace Arkanis.Overlay.Infrastructure.Repositories.Local.Specialised;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Models.Game;

internal class UexPricingRepositorySpecialisationDecorator<T, TEntity>(IGameEntityRepository<T> traitRepository)
    : RepositorySpecialisationDecoratorBase<T>(traitRepository)
    where T : GameEntityPricing<TEntity>
    where TEntity : IGameEntity
{
    private Dictionary<UexApiGameEntityId, T[]> TraitsByItemId { get; set; } = [];

    protected ValueTask<ICollection<T>> GetAllForOwnerAsync(IDomainId domainId, CancellationToken cancellationToken = default)
    {
        if (domainId is not UexId<TEntity> itemId)
        {
            return ValueTask.FromResult<ICollection<T>>([]);
        }

        var traits = TraitsByItemId.GetValueOrDefault(itemId, []);
        return ValueTask.FromResult<ICollection<T>>(traits);
    }

    protected override async Task UpdateAllAsyncCore(CancellationToken cancellationToken)
        => TraitsByItemId = await DecoratedRepository.GetAllAsync(cancellationToken)
            .GroupBy(trait => trait.OwnerId)
            .ToDictionaryAwaitAsync(
                group => ValueTask.FromResult(group.Key),
                group => group.ToArrayAsync(cancellationToken),
                cancellationToken
            );
}
