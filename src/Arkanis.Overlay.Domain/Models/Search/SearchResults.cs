namespace Arkanis.Overlay.Domain.Models.Search;

using Abstractions.Game;

public record GameEntitySearchResults(ICollection<IGameEntity> GameEntities, TimeSpan SearchTime);
