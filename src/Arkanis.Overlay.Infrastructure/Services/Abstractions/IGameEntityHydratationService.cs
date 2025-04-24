namespace Arkanis.Overlay.Infrastructure.Services.Abstractions;

using Domain.Abstractions.Game;

public interface IGameEntityHydrationService
{
    Task HydrateAsync<T>(T gameEntity, CancellationToken cancellationToken = default) where T : IGameEntity;
}
