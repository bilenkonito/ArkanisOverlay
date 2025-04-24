namespace Arkanis.Overlay.Infrastructure.Services.Hydration;

using System.Globalization;
using Abstractions;
using Domain.Abstractions.Services;
using Domain.Models.Game;

public class UexGameItemTraitHydrationService(IGameEntityRepository<GameItemTrait> traitRepository) : IHydrationServiceFor<GameItem>
{
    public async Task HydrateAsync(GameItem entity, CancellationToken cancellationToken = default)
    {
        var itemId = entity.Id.Identity.ToString("0", CultureInfo.InvariantCulture);
        var itemTraits = await traitRepository.GetAllAsync(cancellationToken)
            .Where(trait => entity.Id.Equals(trait.ItemId))
            .ToListAsync(cancellationToken);

        entity.Traits.Update(itemTraits);
    }

    public bool IsReady
        => true;

    public Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}
