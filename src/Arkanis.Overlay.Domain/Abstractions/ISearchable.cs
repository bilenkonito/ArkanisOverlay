namespace Arkanis.Overlay.Domain.Abstractions;

using Enums;

public interface ISearchable
{
    string SearchName { get; }
    GameEntityCategory EntityCategory { get; }
}
