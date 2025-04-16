namespace Arkanis.Overlay.Domain.Options;

using System.Globalization;
using Common.Abstractions;

public class UserOptions : ISelfBindableOptions
{
    public string SectionPath
        => "User";

    public RegionInfo? CustomRegionInfo { get; set; }
}
