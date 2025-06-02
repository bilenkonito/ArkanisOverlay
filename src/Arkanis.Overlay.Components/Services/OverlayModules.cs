namespace Arkanis.Overlay.Components.Services;

using Domain.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;

public class OverlayModules
{
    private readonly ICollection<Entry> _modules =
    [
        new()
        {
            Url = "/search",
            Name = "Search",
            Icon = Icons.Material.Filled.Search,
        },
        new()
        {
            Url = "/inventory",
            Name = "Inventory",
            Icon = Icons.Material.Filled.Warehouse,
            GetUpdateCountAsync = async serviceProvider =>
            {
                var inventoryManager = serviceProvider.GetRequiredService<IInventoryManager>();
                return await inventoryManager.GetUnassignedCountAsync();
            },
        },
    ];

    public ICollection<Entry> GetAll()
        => _modules;

    public class Entry
    {
        public required string Url { get; init; }
        public required string Name { get; init; }

        public string Icon { get; init; } = Icons.Material.Filled.ViewModule;

        public Func<IServiceProvider, ValueTask<int>> GetUpdateCountAsync { get; set; } = _ => ValueTask.FromResult(0);
    }
}
