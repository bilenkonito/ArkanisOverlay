namespace Arkanis.Overlay.Infrastructure.Services.Hydration;

using Abstractions;
using Domain.Abstractions.Services;
using Domain.Models.Game;

public class UexGameItemTraitHydrationService(IGameItemTraitRepository traitRepository) : IHydrationServiceFor<GameItem>
{
    public async Task HydrateAsync(GameItem entity, CancellationToken cancellationToken = default)
    {
        var itemTraits = await traitRepository.GetAllForItemAsync(entity.Id, cancellationToken);
        entity.Traits.Update(itemTraits);
    }

    public bool IsReady
        => traitRepository.IsReady;

    public async Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        => await traitRepository.WaitUntilReadyAsync(cancellationToken);
}
