namespace Arkanis.Overlay.Domain.Abstractions;

public interface IDependable
{
    Task WaitUntilReadyAsync(CancellationToken cancellationToken = default);
}
