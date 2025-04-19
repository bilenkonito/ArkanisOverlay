namespace Arkanis.Overlay.Infrastructure.UnitTests.Data.Mappers;

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

        var source = ExternalUexDTOFixture.OutpostTerminal;
        var result = mapper.ToGameEntity(source);

        result.Parent.ShouldNotBeNull();
    }

    [Fact]
    public void UniverseTerminalDTO_ToGameEntity_Should_Throw_When_Parent_Is_Missing()
    {
        var mapper = new UexApiDtoMapper();

        // cache dependencies
        mapper.ToGameEntity(ExternalUexDTOFixture.StarSystem);
        mapper.ToGameEntity(ExternalUexDTOFixture.Planet);
        mapper.ToGameEntity(ExternalUexDTOFixture.Moon);

        var source = ExternalUexDTOFixture.OutpostTerminal;
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
        Should.Throw<ObjectMappingMissingDependentObjectException>(() => mapper.ToGameEntity(source));
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
