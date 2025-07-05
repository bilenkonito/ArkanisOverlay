namespace Arkanis.Overlay.Domain.Models.Trade;

using System.Diagnostics.CodeAnalysis;
using Abstractions.Game;
using Enums;
using Game;
using Inventory;
using MoreLinq;
using MudBlazor.FontIcons.MaterialIcons;

public record TradeRunId(Guid Identity) : TypedDomainId<Guid>(Identity)
{
    public static TradeRunId CreateNew()
        => new(Guid.NewGuid());
}

[SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
public class TradeRun
{
    public TradeRunId Id { get; init; } = TradeRunId.CreateNew();

    public GameVehicle? Vehicle { get; init; }

    public AcquisitionStage[] Acquisitions { get; set; } = [];
    public List<SaleStage> Sales { get; set; } = [];

    public StarCitizenVersion? Version { get; init; }
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? FinalizedAt { get; set; }

    public Stage? StageInProgress
        => Stages.FirstOrDefault(x => x.FinalizedAt is null);

    public IEnumerable<QuantityOf> AcquiredQuantities
        => Acquisitions
            .Where(x => x is { AcquiredAt: not null, FinalizedAt: not null })
            .GroupBy(x => x.Quantity.Reference.EntityId)
            .Select(amounts => new QuantityOf(
                    amounts.First().Quantity.Reference,
                    amounts.Select(amount => amount.Quantity.Amount).Sum(),
                    amounts.First().Quantity.Unit
                )
            );

    public IEnumerable<QuantityOf> SoldQuantities
        => Sales
            .Where(x => x is { SoldAt: not null, FinalizedAt: not null })
            .GroupBy(x => x.Quantity.Reference.EntityId)
            .Select(amounts => new QuantityOf(
                    amounts.First().Quantity.Reference,
                    amounts.Select(amount => amount.Quantity.Amount).Sum(),
                    amounts.First().Quantity.Unit
                )
            );

    public IEnumerable<QuantityOf> UnsoldQuantities
        => AcquiredQuantities.LeftJoin(
            SoldQuantities,
            acquired => acquired.Reference.EntityId,
            acquired => acquired,
            (acquired, sold) => new QuantityOf(acquired.Reference, acquired.Amount - sold.Amount, acquired.Unit)
        );

    public GameCurrency Investment
        => new(-1 * Acquisitions.Sum(x => x.PriceTotal.Amount));

    public GameCurrency Fees
        => new(Stages.Sum(x => x.Fees.Amount));

    public GameCurrency Revenue
        => new(Sales.Sum(x => x.PriceTotal.Amount));

    public GameCurrency Profit
        => Revenue + Investment + Fees;

    public GameCurrency CurrentInvestment
        => new(-1 * Acquisitions.Where(x => x.AcquiredAt is not null).Sum(x => x.PriceTotal.Amount));

    public GameCurrency CurrentRevenue
        => new(Sales.Where(x => x.SoldAt is not null).Sum(x => x.PriceTotal.Amount));

    public GameCurrency CurrentProfit
        => CurrentRevenue + CurrentInvestment + Fees;

    public IEnumerable<Stage> Stages
        => Acquisitions
            .OfType<Stage>()
            .Concat(Sales);

    public bool HasUnsoldCargo
        => UnsoldQuantities.Any(x => x.Amount > 0);

    public TimeSpan Length
        => (FinalizedAt ?? DateTimeOffset.UtcNow) - CreatedAt;

    public Quantity AcquiredQuantityOf(UexApiGameEntityId gameEntityId)
        => AcquiredQuantities
            .Where(x => x.Reference.EntityId == gameEntityId)
            .SingleOrDefault(Quantity.Zero);

    public static TradeRun Create(GameTradeRoute tradeRoute, Context context)
        => new()
        {
            Vehicle = context.Vehicle,
            Acquisitions = [TerminalPurchaseStage.Create(tradeRoute, context)],
            Sales = [TerminalSaleStage.Create(tradeRoute, context)],
            Version = context.Version,
        };

    public IEnumerable<PlayerEvent> CreateEvents()
    {
        yield return new PlayerEvent.RunStarted(CreatedAt);

        foreach (var playerEvent in Stages.SelectMany(x => x.CreateEvents()).OrderBy(x => x.OccuredAt))
        {
            yield return playerEvent;
        }

        if (FinalizedAt is not null)
        {
            yield return new PlayerEvent.RunCompleted(FinalizedAt.Value);
        }
    }

    public class Context
    {
        public required StarCitizenVersion? Version { get; init; }
        public GameVehicle? Vehicle { get; init; }
        public Quantity Quantity { get; init; } = Quantity.Zero;

        public QuantityOf GetQuantityOf(OwnableEntityReference entityReference)
            => new(entityReference, Quantity.Amount, Quantity.Unit);
    }

    public abstract class PlayerEvent(DateTimeOffset occuredAt)
    {
        public abstract string Text { get; }
        public abstract string Icon { get; }

        public DateTimeOffset OccuredAt { get; } = occuredAt;

        public class RunStarted(DateTimeOffset occuredAt) : PlayerEvent(occuredAt)
        {
            public override string Text
                => "Trade run started";

            public override string Icon
                => Filled.Check;
        }

        public class RunCompleted(DateTimeOffset occuredAt) : PlayerEvent(occuredAt)
        {
            public override string Text
                => "Trade run finalized";

            public override string Icon
                => Filled.Start;
        }

        public class StageCompleted(DateTimeOffset occuredAt) : PlayerEvent(occuredAt)
        {
            public override string Text
                => "Trade part completed";

            public override string Icon
                => Filled.Check;
        }

        public class Takeoff(DateTimeOffset occuredAt) : PlayerEvent(occuredAt)
        {
            public override string Text
                => "Started travelling";

            public override string Icon
                => Filled.FlightTakeoff;
        }

        public class Landing(DateTimeOffset occuredAt) : PlayerEvent(occuredAt)
        {
            public override string Text
                => "Reached destination";

            public override string Icon
                => Filled.FlightLand;
        }

        public class CargoAcquired(QuantityOf quantity, DateTimeOffset occuredAt) : PlayerEvent(occuredAt)
        {
            public override string Text
                => $"Purchased {quantity}";

            public override string Icon
                => Filled.AddShoppingCart;
        }

        public class CargoSold(QuantityOf quantity, DateTimeOffset occuredAt) : PlayerEvent(occuredAt)
        {
            public override string Text
                => $"Sold {quantity}";

            public override string Icon
                => Filled.AttachMoney;
        }

        public class CargoTransferred(QuantityOf quantity, DateTimeOffset occuredAt) : PlayerEvent(occuredAt)
        {
            public override string Text
                => $"Finished transferring {quantity}";

            public override string Icon
                => Filled.Forklift;
        }
    }

    public abstract class Stage
    {
        public int Id { get; init; }

        public GameCurrency PricePerUnit { get; set; } = GameCurrency.Zero;

        public GameCurrency PriceTotal
            => PricePerUnit * Quantity.Amount;

        public virtual GameCurrency Fees
            => CargoTransferFee * -1;

        public abstract GameCurrency Balance { get; }
        public abstract string Title { get; }
        public abstract string CurrentStepTitle { get; }

        public required QuantityOf Quantity { get; set; }

        public GameCurrency CargoTransferFee { get; set; } = GameCurrency.Zero;
        public bool CargoTransferAutomatic { get; set; }

        public DateTimeOffset? StartedAt { get; set; }
        public DateTimeOffset? ReachedAt { get; set; }
        public DateTimeOffset? TransferredAt { get; set; }
        public DateTimeOffset? FinalizedAt { get; set; }

        public abstract IEnumerable<PlayerEvent> CreateEvents();
    }

    public abstract class AcquisitionStage : Stage
    {
        public DateTimeOffset? AcquiredAt { get; set; }

        public override GameCurrency Balance
            => (PricePerUnit * Quantity.Amount * -1) + Fees;

        public override string CurrentStepTitle
            => this switch
            {
                { ReachedAt: null } => "Travel to the destination",
                { AcquiredAt: null } => "Acquire the cargo",
                { TransferredAt: null } => "Load the acquired cargo",
                { FinalizedAt: null } => "Prepare for takeoff",
                { FinalizedAt: not null } => "This stage has been finished",
                _ => "Continue",
            };

        public override IEnumerable<PlayerEvent> CreateEvents()
        {
            if (StartedAt is not null)
            {
                yield return new PlayerEvent.Takeoff(StartedAt.Value);
            }

            if (ReachedAt is not null)
            {
                yield return new PlayerEvent.Landing(ReachedAt.Value);
            }

            if (AcquiredAt is not null)
            {
                yield return new PlayerEvent.CargoAcquired(Quantity, AcquiredAt.Value);
            }

            if (TransferredAt is not null)
            {
                yield return new PlayerEvent.CargoTransferred(Quantity, TransferredAt.Value);
            }

            if (FinalizedAt is not null)
            {
                yield return new PlayerEvent.StageCompleted(FinalizedAt.Value);
            }
        }
    }

    public sealed class TerminalPurchaseStage : AcquisitionStage, IGameLocatedAt
    {
        public override string Title
            => $"Purchase {Quantity.ToString(QuantityOf.FormatWithReferenceCode, null)} at {Terminal.Name.MainContent.FullName}";

        public required GameTerminal Terminal { get; set; }

        public TerminalData UserSourcedData { get; set; } = new();

        IGameLocation IGameLocatedAt.Location
            => Terminal;

        public static TerminalPurchaseStage Create(GameTradeRoute tradeRoute, Context context)
        {
            var commodity = new OwnableEntityReference.Commodity(tradeRoute.Commodity);
            var newStockAmount = Math.Min(tradeRoute.Origin.CargoUnitsAvailable - context.Quantity.Amount, 0);
            var newStock = Inventory.Quantity.FromScu(newStockAmount);

            return new TerminalPurchaseStage
            {
                PricePerUnit = tradeRoute.Origin.Price,
                UserSourcedData =
                {
                    MaxContainerSize = tradeRoute.Origin.MaxContainerSize,
                    StockStatus = newStockAmount == 0
                        ? TerminalInventoryStatus.OutOfStock
                        : tradeRoute.Origin.InventoryStatus,
                    Stock = newStock,
                },
                Terminal = tradeRoute.Origin.Terminal,
                Quantity = context.GetQuantityOf(commodity),
            };
        }
    }

    public abstract class SaleStage : Stage
    {
        public DateTimeOffset? SoldAt { get; set; }

        public override GameCurrency Balance
            => (PricePerUnit * Quantity.Amount) + Fees;

        public override string CurrentStepTitle
            => this switch
            {
                { ReachedAt: null } => "Travel to the destination",
                { TransferredAt: null } when !CargoTransferAutomatic => "Unload the acquired cargo",
                { SoldAt: null } => "Sell the cargo",
                { TransferredAt: null } => "Wait for the cargo to unload",
                { FinalizedAt: null } => "Prepare for takeoff",
                { FinalizedAt: not null } => "This stage has been finished",
                _ => "Continue",
            };

        public override IEnumerable<PlayerEvent> CreateEvents()
        {
            if (StartedAt is not null)
            {
                yield return new PlayerEvent.Takeoff(StartedAt.Value);
            }

            if (ReachedAt is not null)
            {
                yield return new PlayerEvent.Landing(ReachedAt.Value);
            }

            if (SoldAt is not null)
            {
                yield return new PlayerEvent.CargoSold(Quantity, SoldAt.Value);
            }

            if (TransferredAt is not null)
            {
                yield return new PlayerEvent.CargoTransferred(Quantity, TransferredAt.Value);
            }

            if (FinalizedAt is not null)
            {
                yield return new PlayerEvent.StageCompleted(FinalizedAt.Value);
            }
        }
    }

    public sealed class TerminalSaleStage : SaleStage, IGameLocatedAt
    {
        public override string Title
            => $"Sell {Quantity.ToString(QuantityOf.FormatWithReferenceCode, null)} at {Terminal.Name.MainContent.FullName}";

        public required GameTerminal Terminal { get; set; }

        public TerminalData UserSourcedData { get; set; } = new();

        IGameLocation IGameLocatedAt.Location
            => Terminal;

        public static TerminalSaleStage Create(GameTradeRoute tradeRoute, Context context)
        {
            var commodity = new OwnableEntityReference.Commodity(tradeRoute.Commodity);
            var currentStock = Inventory.Quantity.FromScu(tradeRoute.Destination.CargoUnitsAvailable);

            return new TerminalSaleStage
            {
                PricePerUnit = tradeRoute.Destination.Price,
                UserSourcedData =
                {
                    MaxContainerSize = tradeRoute.Destination.MaxContainerSize,
                    StockStatus = tradeRoute.Destination.InventoryStatus,
                    Stock = currentStock + context.Quantity,
                },
                Terminal = tradeRoute.Destination.Terminal,
                Quantity = context.GetQuantityOf(commodity),
            };
        }
    }
}
