namespace Arkanis.Overlay.Domain.Models.Game;

public record GameDataState;

public sealed record SyncedGameDataState(StarCitizenVersion Version, DateTimeOffset UpdatedAt) : GameDataState;

public sealed record MissingGameDataState : GameDataState
{
    public static readonly GameDataState Instance = new MissingGameDataState();
}
