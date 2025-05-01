namespace Arkanis.Overlay.Components.Services.Abstractions;

using Arkanis.Overlay.Domain.Models.Keyboard;
using Microsoft.AspNetCore.Components.Web;

public interface IKeyboardProxy
{
    event EventHandler<KeyboardKey> OnKeyUp;
    event EventHandler<KeyboardKey> OnKeyDown;
    event EventHandler<KeyboardShortcut> OnKeyboardShortcut;

    void RegisterKeyUp(KeyboardEventArgs keyboardEvent);
    void RegisterKeyDown(KeyboardEventArgs keyboardEvent);
}
