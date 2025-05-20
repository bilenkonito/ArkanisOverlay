namespace Arkanis.Overlay.External.UEX;

internal partial class UexGameApi
{
    public UexGameApi(IHttpClientFactory httpClientFactory, UexApiOptions? options = null) : base(httpClientFactory, options)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexCrewApi
{
    public UexCrewApi(IHttpClientFactory httpClientFactory, UexApiOptions? options = null) : base(httpClientFactory, options)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexCommoditiesApi
{
    public UexCommoditiesApi(IHttpClientFactory httpClientFactory, UexApiOptions? options = null) : base(httpClientFactory, options)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexFuelApi
{
    public UexFuelApi(IHttpClientFactory httpClientFactory, UexApiOptions? options = null) : base(httpClientFactory, options)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexItemsApi
{
    public UexItemsApi(IHttpClientFactory httpClientFactory, UexApiOptions? options = null) : base(httpClientFactory, options)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexMarketplaceApi
{
    public UexMarketplaceApi(IHttpClientFactory httpClientFactory, UexApiOptions? options = null) : base(httpClientFactory, options)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexOrganizationsApi
{
    public UexOrganizationsApi(IHttpClientFactory httpClientFactory, UexApiOptions? options = null) : base(httpClientFactory, options)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexRefineriesApi
{
    public UexRefineriesApi(IHttpClientFactory httpClientFactory, UexApiOptions? options = null) : base(httpClientFactory, options)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexStaticApi
{
    public UexStaticApi(IHttpClientFactory httpClientFactory, UexApiOptions? options = null) : base(httpClientFactory, options)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexUserApi
{
    public UexUserApi(IHttpClientFactory httpClientFactory, UexApiOptions? options = null) : base(httpClientFactory, options)
        => BaseUrl = UexConstants.BaseUrl;
}

internal partial class UexVehiclesApi
{
    public UexVehiclesApi(IHttpClientFactory httpClientFactory, UexApiOptions? options = null) : base(httpClientFactory, options)
        => BaseUrl = UexConstants.BaseUrl;
}
