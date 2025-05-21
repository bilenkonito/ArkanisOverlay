namespace Arkanis.Overlay.Host.Desktop.Services;

using Microsoft.Extensions.Logging;
using Velopack;
using Velopack.Locators;
using Velopack.Sources;

public class ArkanisOverlayUpdateManager(
    IUpdateSource source,
    ILogger<ArkanisOverlayUpdateManager> logger,
    UpdateOptions? options = null,
    IVelopackLocator? locator = null
) : UpdateManager(source, options, logger, locator)
{
    public string CurrentChannel
        => Channel;
}
