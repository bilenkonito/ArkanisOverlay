namespace Arkanis.Overlay.Common.Abstractions.Services;

/// <summary>
///     This service requires initialization before it can be used.
///     It can initialize itself, but it may also depend on other services to be initialized.
/// </summary>
public interface ISelfInitializable : IDependable
{
    Task InitializeAsync(CancellationToken cancellationToken);
}
