namespace Arkanis.Overlay.Infrastructure.Services.Abstractions;

using Microsoft.Extensions.Primitives;

public interface IChangeTokenManager
{
    /// <summary>
    ///     Gets the <see cref="IChangeToken" /> associated with the given type.
    /// </summary>
    /// <typeparam name="T">Type to get the change token for.</typeparam>
    /// <returns>The <see cref="IChangeToken" /> associated with the given type.</returns>
    IChangeToken GetChangeTokenFor<T>();

    /// <summary>
    ///     Triggers a change to the <see cref="IChangeToken" /> associated with the given type and returns the new
    ///     <see cref="IChangeToken" />.
    /// </summary>
    /// <typeparam name="T">Type to reset the change token for.</typeparam>
    /// <returns>The new <see cref="IChangeToken" /> associated with the given type.</returns>
    Task<IChangeToken> ResetChangeTokenFor<T>();

    /// <summary>
    ///     Triggers a change to the <see cref="IChangeToken" /> associated with the given type.
    /// </summary>
    /// <typeparam name="T">Type to reset the change token for.</typeparam>
    Task TriggerChangeForAsync<T>();
}
