namespace Arkanis.Overlay.Domain.Models;

using Game;

public abstract record AppDataState;

public sealed record AppDataUpToDate(GameDataState DataState) : AppDataState;

public sealed record AppDataOutOfDate(GameDataState DataState) : AppDataState;

public sealed record AppDataMissing : AppDataState
{
    public static readonly AppDataState Instance = new AppDataMissing();
}
