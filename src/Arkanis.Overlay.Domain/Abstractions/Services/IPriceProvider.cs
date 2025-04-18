namespace Arkanis.Overlay.Domain.Abstractions.Services;

public interface IPriceProvider
    : IPurchasePriceProvider,
        ISellPriceProvider,
        IRentPriceProvider;
