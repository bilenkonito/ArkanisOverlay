namespace Arkanis.Overlay.External.UEX;

using System.Net.Http.Headers;

internal partial class UexGameApi
{
    public UexGameApi(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexCrewApi
{
    public UexCrewApi(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexCommoditiesApi
{
    public UexCommoditiesApi(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexFuelApi
{
    public UexFuelApi(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexItemsApi
{
    public UexItemsApi(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexMarketplaceApi
{
    public UexMarketplaceApi(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        => BaseUrl = UexConstants.BaseUrl;

    partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
        => client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "bf2b6a87035311d6f39385efab26c65a4a8519ef");
}

internal partial class UexOrganizationsApi
{
    public UexOrganizationsApi(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexRefineriesApi
{
    public UexRefineriesApi(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexStaticApi
{
    public UexStaticApi(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexUserApi
{
    public UexUserApi(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexVehiclesApi
{
    public UexVehiclesApi(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        => BaseUrl = UexConstants.BaseUrl;
}
