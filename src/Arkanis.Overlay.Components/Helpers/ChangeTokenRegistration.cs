namespace Arkanis.Overlay.Components.Helpers;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Primitives;

public sealed class ChangeTokenRegistration : IDisposable
{
    public delegate void ChangeCallback();

    private readonly Func<IChangeToken> _getChangeToken;
    private IDisposable? _currentRegistration;

    public ChangeTokenRegistration(Func<IChangeToken> getChangeToken)
    {
        _getChangeToken = getChangeToken;
        UpdateRegistration();
    }

    public void Dispose()
        => _currentRegistration?.Dispose();

    public event ChangeCallback? OnChange;

    private void ChangeHandler(object? _)
        => UpdateRegistration();

    [MemberNotNull(nameof(_currentRegistration))]
    private void UpdateRegistration()
    {
        _currentRegistration?.Dispose();
        _currentRegistration = _getChangeToken().RegisterChangeCallback(ChangeHandler, null);
        OnChange?.Invoke();
    }
}
