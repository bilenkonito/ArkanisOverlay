namespace Arkanis.Overlay.Infrastructure.Helpers;

using Microsoft.AspNetCore.Http;

public static class ExternalLinkHelper
{
    private static string AddPartnerGoogleAnalyticsTo(string url, string? contentId = null)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["utm_source"] = "Arkanis",
            ["utm_medium"] = "partner",
            ["utm_campaign"] = "Partnership",
        };
        if (contentId is not null)
        {
            queryParams["utm_content"] = contentId;
        }

        return url + QueryString.Create(queryParams);
    }

    public static string GetUexLink(string? contentId = null)
        => AddPartnerGoogleAnalyticsTo("https://uexcorp.space/", contentId);

    public static string GetUexUserLink(string username, string? contentId = null)
        => AddPartnerGoogleAnalyticsTo($"https://uexcorp.space/@{username}", contentId);
}
