namespace Arkanis.Overlay.Infrastructure.Options;

using System.ComponentModel.DataAnnotations;
using Common.Abstractions;

public class ConfigurationOptions : ISelfBindableOptions
{
    [Required]
    [Url]
    public required string UexBaseUrl { get; set; } = "https://api.uexcorp.space/2.0/";

    public string SectionPath
        => "Configuration";
}
