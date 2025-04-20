namespace Arkanis.Overlay.Domain.Abstractions;

using Models.Search;

public interface ISearchable
{
    IEnumerable<SearchableAttribute> SearchableAttributes { get; }
}
