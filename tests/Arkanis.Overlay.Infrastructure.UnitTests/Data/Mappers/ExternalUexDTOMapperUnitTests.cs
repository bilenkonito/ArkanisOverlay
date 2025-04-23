namespace Arkanis.Overlay.Infrastructure.UnitTests.Data.Mappers;

using Domain.Enums;
using Domain.Models.Game;
using Infrastructure.Data.Exceptions;
using Infrastructure.Data.Mappers;
using Shouldly;

public class ExternalUexDTOMapperUnitTests
{
    [Fact]
    public void UniverseStartSystemDTO_ToGameEntity_Should_Correctly_Map_Without_Dependencies()
    {
        var mapper = new UexApiDtoMapper();
        var source = ExternalUexDTOFixture.StarSystem;

        var result = mapper.ToGameEntity(source);
        result.Parent.ShouldBeNull();
        result.Name.ShouldHaveSingleItem()
            .ShouldBeEquivalentTo(new GameEntityName.NameWithCode(source.Name!, source.Code!));
    }

    [Fact]
    public void UniverseTerminalDTO_ToGameEntity_Should_Correctly_Map_And_Link_Dependencies()
    {
        var mapper = new UexApiDtoMapper();

        // cache dependencies, order is important
        mapper.ToGameEntity(ExternalUexDTOFixture.StarSystem);
        mapper.ToGameEntity(ExternalUexDTOFixture.Planet);
        mapper.ToGameEntity(ExternalUexDTOFixture.Moon);
        mapper.ToGameEntity(ExternalUexDTOFixture.Outpost);

        var source = ExternalUexDTOFixture.OutpostCommodityTerminal;
        var result = mapper.ToGameEntity(source);

        result.Type.ShouldBe(GameTerminalType.Commodity);
        result.EntityCategory.ShouldBe(GameEntityCategory.Location);
        result.Parent.ShouldNotBeNull() // outpost
            .Parent.ShouldNotBeNull() // moon
            .Parent.ShouldNotBeNull() // planet
            .Parent.ShouldNotBeNull(); // star system
    }

    [Fact]
    public void UniverseTerminalDTO_ToGameEntity_Should_Throw_When_Parent_Is_Missing()
    {
        var mapper = new UexApiDtoMapper();

        // cache dependencies
        mapper.ToGameEntity(ExternalUexDTOFixture.StarSystem);
        mapper.ToGameEntity(ExternalUexDTOFixture.Planet);
        mapper.ToGameEntity(ExternalUexDTOFixture.Moon);

        var source = ExternalUexDTOFixture.OutpostCommodityTerminal;
        Should.Throw<ObjectMappingMissingDependentObjectException>(() => mapper.ToGameEntity(source));
    }

    [Fact]
    public void ItemDTO_ToGameEntity_Should_Correctly_Map_And_Link_Dependencies()
    {
        var mapper = new UexApiDtoMapper();

        // cache dependencies, order is important
        mapper.ToGameEntity(ExternalUexDTOFixture.ItemCompany);
        mapper.ToGameEntity(ExternalUexDTOFixture.ItemHatsCategory);

        var source = ExternalUexDTOFixture.Item;
        var result = mapper.ToGameEntity(source);
        result.Manufacturer.ShouldNotBeNull();
        result.TerminalType.ShouldBe(GameTerminalType.Item);
        result.EntityCategory.ShouldBe(GameEntityCategory.Item);
    }

    [Fact]
    public void ItemDTO_ToGameEntity_Should_Throw_When_Parent_Is_Missing()
    {
        var mapper = new UexApiDtoMapper();

        // cache dependencies
        mapper.ToGameEntity(ExternalUexDTOFixture.StarSystem);
        mapper.ToGameEntity(ExternalUexDTOFixture.Planet);
        mapper.ToGameEntity(ExternalUexDTOFixture.Moon);

        var source = ExternalUexDTOFixture.Item;
        Should.Throw<ObjectMappingMissingDependentObjectException>(() => mapper.ToGameEntity(source));
    }
}
