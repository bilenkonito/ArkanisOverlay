namespace Arkanis.Overlay.Domain.Abstractions;

using Game;

public interface IIdentifiable
{
    IDomainId Id { get; }
}
