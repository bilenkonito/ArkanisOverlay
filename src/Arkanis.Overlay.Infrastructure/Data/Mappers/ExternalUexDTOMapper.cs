namespace Arkanis.Overlay.Infrastructure.Data.Mappers;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Game;
using Domain.Models.Trade;
using Exceptions;
using External.UEX.Abstractions;
using Riok.Mapperly.Abstractions;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
[SuppressMessage("Performance", "CA1859:Use concrete types when possible for improved performance")]
internal partial class ExternalUexDTOMapper
{
    internal readonly Dictionary<string, GameEntity> CachedGameEntities = [];

    [UserMapping(Default = true)]
    public GameStarSystem ToGameEntity(UniverseStarSystemDTO source)
    {
        var result = MapInternal(source);
        CacheGameEntityId<GameStarSystem>(source.Id, result);
        return result;
    }

    [UserMapping(Default = true)]
    public GamePlanet ToGameEntity(UniversePlanetDTO source)
    {
        var result = MapInternal(source);
        CacheGameEntityId<GamePlanet>(source.Id, result);
        return result;
    }

    [UserMapping(Default = true)]
    public GameMoon ToGameEntity(UniverseMoonDTO source)
    {
        var result = MapInternal(source);
        CacheGameEntityId<GameMoon>(source.Id, result);
        return result;
    }

    [UserMapping(Default = true)]
    public GameCity ToGameEntity(UniverseCityDTO source)
    {
        var result = MapInternal(source);
        CacheGameEntityId<GameOutpost>(source.Id, result);
        return result;
    }

    [UserMapping(Default = true)]
    public GameOutpost ToGameEntity(UniverseOutpostDTO source)
    {
        var result = MapInternal(source);
        CacheGameEntityId<GameOutpost>(source.Id, result);
        return result;
    }

    [UserMapping(Default = true)]
    public GameTerminal ToGameEntity(UniverseTerminalDTO source)
    {
        var result = MapInternal(source);
        CacheGameEntityId<GameTerminal>(source.Id, result);
        return result;
    }

    [UserMapping(Default = true)]
    public GameCommodity ToGameEntity(CommodityDTO source)
    {
        var result = MapInternal(source);
        CacheGameEntityId<GameCommodity>(source.Id, result);
        return result;
    }

    [UserMapping(Default = true)]
    public GameItem ToGameEntity(ItemDTO source)
    {
        var result = MapInternal(source);
        CacheGameEntityId<GameItem>(source.Id, result);
        return result;
    }

    [UserMapping(Default = true)]
    public GameCompany ToGameEntity(CompanyDTO source)
    {
        var result = MapInternal(source);
        CacheGameEntityId<GameItem>(source.Id, result);
        return result;
    }

    [UserMapping(Default = true)]
    public GameItemCategory ToGameEntity(CategoryDTO source)
    {
        var result = MapInternal(source);
        CacheGameEntityId<GameItem>(source.Id, result);
        return result;
    }

    internal void CacheGameEntityId<T>(double? sourceId, GameEntity result) where T : GameEntity
        => CachedGameEntities[CreateCacheEntityKey<T>(sourceId)] = result;

    internal T? ResolveCachedGameEntity<T>(double? sourceId) where T : GameEntity
    {
        var cacheEntityKey = CreateCacheEntityKey<T>(sourceId);
        return CachedGameEntities.GetValueOrDefault(cacheEntityKey) as T;
    }

    internal static string CreateCacheEntityKey<T>(double? sourceId)
        => $"{typeof(T).Name}-{sourceId}";

    [MapperIgnoreTarget(nameof(GameEntity.Id))]
    [MapperIgnoreTarget(nameof(GameEntity.Name))]
    [MapProperty(nameof(UniverseStarSystemDTO.Name), "fullName")]
    [MapProperty(nameof(UniverseStarSystemDTO.Code), "codeName")]
    private partial GameStarSystem MapInternal(UniverseStarSystemDTO source);

    [MapperIgnoreTarget(nameof(GameEntity.Id))]
    [MapperIgnoreTarget(nameof(GameEntity.Name))]
    [MapProperty(nameof(UniversePlanetDTO.Name), "fullName")]
    [MapProperty(nameof(UniversePlanetDTO.Code), "codeName")]
    [MapPropertyFromSource("location", Use = nameof(GetGameLocationForPlanet))]
    private partial GamePlanet MapInternal(UniversePlanetDTO source);

    [MapperIgnoreTarget(nameof(GameEntity.Id))]
    [MapperIgnoreTarget(nameof(GameEntity.Name))]
    [MapProperty(nameof(UniverseMoonDTO.Name), "fullName")]
    [MapProperty(nameof(UniverseMoonDTO.Code), "codeName")]
    [MapPropertyFromSource("location", Use = nameof(GetGameLocationForMoon))]
    private partial GameMoon MapInternal(UniverseMoonDTO source);

    [MapperIgnoreTarget(nameof(GameEntity.Id))]
    [MapperIgnoreTarget(nameof(GameEntity.Name))]
    [MapProperty(nameof(UniverseCityDTO.Name), "fullName")]
    [MapProperty(nameof(UniverseCityDTO.Code), "codeName")]
    [MapPropertyFromSource("location", Use = nameof(GetGameLocationForCity))]
    private partial GameCity MapInternal(UniverseCityDTO source);

    [MapperIgnoreTarget(nameof(GameEntity.Id))]
    [MapperIgnoreTarget(nameof(GameEntity.Name))]
    [MapProperty(nameof(UniverseOutpostDTO.Name), "fullName")]
    [MapProperty(nameof(UniverseOutpostDTO.Nickname), "shortName")]
    [MapPropertyFromSource("location", Use = nameof(GetGameLocationForOutpost))]
    private partial GameOutpost MapInternal(UniverseOutpostDTO source);

    [MapperIgnoreTarget(nameof(GameEntity.Id))]
    [MapperIgnoreTarget(nameof(GameEntity.Name))]
    [MapProperty(nameof(UniverseTerminalDTO.Name), "fullName")]
    [MapProperty(nameof(UniverseTerminalDTO.Nickname), "shortName")]
    [MapProperty(nameof(UniverseTerminalDTO.Code), "codeName")]
    [MapPropertyFromSource("location", Use = nameof(GetGameLocationForTerminal))]
    private partial GameTerminal MapInternal(UniverseTerminalDTO source);

    [MapperIgnoreTarget(nameof(GameEntity.Id))]
    [MapperIgnoreTarget(nameof(GameEntity.Name))]
    [MapProperty(nameof(CommodityDTO.Name), "fullName")]
    [MapProperty(nameof(CommodityDTO.Code), "codeName")]
    [MapPropertyFromSource(nameof(GameCommodity.LatestBuyPrices), Use = nameof(GetLatestBuyPricesForCommodity))]
    [MapPropertyFromSource(nameof(GameCommodity.LatestSellPrices), Use = nameof(GetLatestSellPricesForCommodity))]
    private partial GameCommodity MapInternal(CommodityDTO source);

    [MapperIgnoreTarget(nameof(GameEntity.Id))]
    [MapperIgnoreTarget(nameof(GameEntity.Name))]
    [MapProperty(nameof(ItemDTO.Uuid), "uuid")]
    [MapProperty(nameof(ItemDTO.Name), "fullName")]
    [MapPropertyFromSource("manufacturer", Use = nameof(GetCompanyForItem))]
    [MapPropertyFromSource(nameof(GameItem.LatestBuyPrices), Use = nameof(GetLatestBuyPricesForItem))]
    private partial GameItem MapInternal(ItemDTO source);

    [MapperIgnoreTarget(nameof(GameEntity.Id))]
    [MapperIgnoreTarget(nameof(GameEntity.Name))]
    [MapProperty(nameof(CompanyDTO.Name), "fullName")]
    [MapProperty(nameof(CompanyDTO.Nickname), "shortName")]
    private partial GameCompany MapInternal(CompanyDTO source);

    [MapperIgnoreTarget(nameof(GameEntity.Id))]
    [MapperIgnoreTarget(nameof(GameEntity.Name))]
    [MapProperty(nameof(CategoryDTO.Name), "fullName")]
    [MapProperty(nameof(CategoryDTO.Section), "section")]
    private partial GameItemCategory MapInternal(CategoryDTO source);

    [UserMapping(Default = true)]
    private static GameTerminalType MapInternal(string source)
        => source switch
        {
            "commodity" => GameTerminalType.Commodity,
            "item" => GameTerminalType.Item,
            "fuel" => GameTerminalType.Fuel,
            "vehicle_buy" => GameTerminalType.VehicleBuy,
            "vehicle_rent" => GameTerminalType.VehicleRent,
            _ => Enum.Parse<GameTerminalType>(source, true),
        };

    [DoesNotReturn]
    private static T ThrowInvalidCacheException<T>(double? sourceId, [CallerArgumentExpression("sourceId")] string sourceIdExpression = "")
        => throw new ObjectMappingMissingDependentObjectException(
            $"Could not resolve cached entity instance of {typeof(T).Name} for: {sourceIdExpression} == {sourceId}"
        );

    private GameLocationEntity GetGameLocationForPlanet(UniversePlanetDTO planet)
        => ResolveCachedGameEntity<GameStarSystem>(planet.Id_star_system)
           ?? ThrowInvalidCacheException<GameStarSystem>(planet.Id_star_system);

    private GameLocationEntity GetGameLocationForMoon(UniverseMoonDTO moon)
        => ResolveCachedGameEntity<GamePlanet>(moon.Id_planet) as GameLocationEntity
           ?? ResolveCachedGameEntity<GameStarSystem>(moon.Id_star_system)
           ?? ThrowInvalidCacheException<GameStarSystem>(moon.Id_star_system);

    private GameLocationEntity GetGameLocationForCity(UniverseCityDTO city)
        => ResolveCachedGameEntity<GamePlanet>(city.Id_planet) as GameLocationEntity
           ?? ResolveCachedGameEntity<GameMoon>(city.Id_moon)
           ?? ThrowInvalidCacheException<GameMoon>(city.Id_moon);

    private GameLocationEntity GetGameLocationForOutpost(UniverseOutpostDTO outpost)
        => ResolveCachedGameEntity<GamePlanet>(outpost.Id_planet) as GameLocationEntity
           ?? ResolveCachedGameEntity<GameMoon>(outpost.Id_moon)
           ?? ThrowInvalidCacheException<GameMoon>(outpost.Id_moon);

    private GameLocationEntity GetGameLocationForTerminal(UniverseTerminalDTO terminal)
        => ResolveCachedGameEntity<GameCity>(terminal.Id_city) as GameLocationEntity
           ?? ResolveCachedGameEntity<GameOutpost>(terminal.Id_outpost)
           ?? ThrowInvalidCacheException<GameOutpost>(terminal.Id_outpost);

    private GameCompany GetCompanyForItem(ItemDTO item)
        => ResolveCachedGameEntity<GameCompany>(item.Id_company)
           ?? ThrowInvalidCacheException<GameCompany>(item.Id_company);

    private Bounds<PriceTag> GetLatestBuyPricesForItem(ItemDTO item)
        => new(PriceTag.Unknown, PriceTag.Unknown, PriceTag.Unknown);

    private Bounds<PriceTag> GetLatestBuyPricesForCommodity(CommodityDTO commodity)
        => new(PriceTag.Unknown, PriceTag.Unknown, PriceTag.Unknown);

    private Bounds<PriceTag> GetLatestSellPricesForCommodity(CommodityDTO commodity)
        => new(PriceTag.Unknown, PriceTag.Unknown, PriceTag.Unknown);
}
