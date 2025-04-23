namespace Arkanis.Overlay.Infrastructure.Services.Hydration;

using System.Globalization;
using Abstractions;
using Domain.Models.Game;
using External.UEX.Abstractions;

public class UexGameItemTraitHydrationService(IUexItemsApi itemsApi) : IHydrationServiceFor<GameItem>
{
    public async Task HydrateAsync(GameItem entity, CancellationToken cancellationToken = default)
    {
        var itemId = entity.Id.Identity.ToString("0", CultureInfo.InvariantCulture);
        var itemAttributes = await itemsApi.GetItemsAttributesByItemAsync(itemId, cancellationToken);
        var traits = itemAttributes.Result.Data?
            .Select(attribute => GameItem.Trait.CreateFrom(attribute.Attribute_name ?? string.Empty, attribute.Value ?? string.Empty, attribute.Unit))
            .Where(trait => trait is not null)
            .Select(trait => trait!)
            .ToList();

        entity.Traits.Update(traits ?? []);
    }

    public bool IsReady
        => true;

    public Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}
