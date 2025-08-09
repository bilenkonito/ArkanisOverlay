namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Game;
using Microsoft.Extensions.Primitives;
using Models.Inventory;

public interface IInventoryManager
{
    IChangeToken ChangeToken { get; }

    Task<int> GetUnassignedCountAsync(CancellationToken cancellationToken = default);

    Task<ICollection<InventoryEntryBase>> GetEntriesForAsync(
        IDomainId domainId,
        InventoryEntryBase.EntryType entryType,
        CancellationToken cancellationToken = default
    );

    Task<ICollection<InventoryEntryBase>> GetEntriesForAsync(IDomainId domainId, CancellationToken cancellationToken = default);

    Task<ICollection<InventoryEntryBase>> GetAllEntriesAsync(CancellationToken cancellationToken = default);

    Task AddOrUpdateEntryAsync(InventoryEntryBase entry, CancellationToken cancellationToken = default);

    Task DeleteEntryAsync(InventoryEntryId entryId, CancellationToken cancellationToken = default);

    Task<InventoryEntryList?> GetListAsync(InventoryEntryListId listId, CancellationToken cancellationToken = default);

    Task<ICollection<InventoryEntryList>> GetAllListsAsync(CancellationToken cancellationToken = default);

    Task AddOrUpdateListAsync(InventoryEntryList list, CancellationToken cancellationToken = default);

    Task DeleteListAsync(InventoryEntryListId listId, CancellationToken cancellationToken = default);
}
