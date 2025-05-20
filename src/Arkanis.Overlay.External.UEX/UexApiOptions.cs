namespace Arkanis.Overlay.External.UEX;

using System.ComponentModel.DataAnnotations;

public class UexApiOptions
{
    [Url]
    public string BaseUrl { get; set; } = UexConstants.BaseUrl;

    public string? ApplicationToken { get; set; }

    public string? UserToken { get; set; }
}
