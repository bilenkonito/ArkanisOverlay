namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Models.Game;
using External.UEX.Abstractions;

internal class UexCompanyRepository(IUexGameApi gameApi, IUexStaticApi staticApi, UexApiDtoMapper mapper)
    : UexGameEntityRepositoryBase<CompanyDTO, GameCompany>(staticApi, mapper)
{
    protected override async Task<ICollection<CompanyDTO>> GetAllInternalAsync(CancellationToken cancellationToken)
    {
        var response = await gameApi.GetCompaniesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return response.Result.Data ?? ThrowCouldNotParseResponse();
    }

    protected override double? GetSourceApiId(CompanyDTO source)
        => source.Id;
}
